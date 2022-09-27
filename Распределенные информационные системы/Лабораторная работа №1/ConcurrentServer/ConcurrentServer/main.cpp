#include <WinSock2.h>
#include <WS2tcpip.h>
#pragma comment(lib, "WS2_32.lib")
#include <iostream>
#include <list>
#include <string.h>
#include "CMDCommand.h"
#include "AnotherFunc.h"
#include "Structs.h"
#include "../ServiceLib/Services.h"

#define AS_SQUIRT 10

using namespace std;

ServerParams serverParams{};
CMDCommand global_variance_of_cmd_command = startcmd;
SOCKET sS;
list<Contact> ListContact; // список подключений
CRITICAL_SECTION listCS;
ServerStats serverStats;

void ReduceArgs(int argc, char* argv[])
{
	switch (argc)
	{
	case 1:
		serverParams.port = 2000;
		serverParams.pipeName = "ConsolePipe";
		serverParams.dllName = "ServiceLib.dll";
		serverParams.serviceName = "ConcurrentServer";
		serverParams.responsePort = 2001;
		break;
	case 2:
		serverParams.port = atoi(argv[1]);
		serverParams.dllName = "ServiceLib.dll";
		break;
	case 3:
		serverParams.port = atoi(argv[1]);
		serverParams.dllName = "ServiceLib.dll";
		break;
	case 4:
		serverParams.port = atoi(argv[1]);
		serverParams.pipeName = argv[2];
		serverParams.dllName = "ServiceLib.dll";
		break;
	case 5:
		serverParams.port = atoi(argv[1]);
		serverParams.pipeName = argv[2];
		serverParams.serviceName = argv[3];
		serverParams.dllName = "ServiceLib.dll";
		break;
	case 6:
		serverParams.port = atoi(argv[1]);
		serverParams.pipeName = argv[2];
		serverParams.serviceName = argv[3];
		serverParams.responsePort = atoi(argv[4]);
		serverParams.dllName = "ServiceLib.dll";
		break;
	default:
		std::cout << "Wrong parameters count.... Exit";
		throw exception("wrong parameters count");
	}
}

void CommandsCycle(CMDCommand* cmd);
bool AcceptCycle(int squirt);
string CommandReducer(string msg, Contact* contact, void* (*serveClient)(const char*, LPVOID));

// ------------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------------

DWORD WINAPI AcceptServerThread(LPVOID command_param)
{
	auto command = static_cast<CMDCommand*>(command_param);

	try
	{
		HANDLE acceptEvent = CreateEvent(NULL, true, false, L"ClientAccept");
		if (!acceptEvent)
			throw L"AcceptThread-CreateEvent";

		WSAData wData;
		if (WSAStartup(MAKEWORD(2, 2), &wData) != 0) {
			throw GetMessageError(WSAGetLastError());
		}

		SOCKADDR_IN addr;
		int addrl = sizeof(addr);
		addr.sin_addr.S_un.S_addr = INADDR_ANY;
		addr.sin_port = htons(serverParams.port);
		addr.sin_family = AF_INET;

		if ((sS = socket(AF_INET, SOCK_STREAM, NULL)) == SOCKET_ERROR) {
			throw GetMessageError(WSAGetLastError());
		}

		if (bind(sS, (struct sockaddr*) & addr, sizeof(addr)) == SOCKET_ERROR) {
			throw GetMessageError(WSAGetLastError());
		}

		if (listen(sS, 10) == SOCKET_ERROR) {
			throw GetMessageError(WSAGetLastError());
		}

		u_long nonblk;
		if (ioctlsocket(sS, FIONBIO, &(nonblk = 1)) == SOCKET_ERROR)
			throw GetMessageError(WSAGetLastError());

		CommandsCycle(command);

		closesocket(sS);
		WSACleanup();
	}
	catch (string str)
	{
		std::cout << str << endl;
	}

	return 0;
}

void CommandsCycle(CMDCommand* cmd) // цикл обработки команд
{
	int squirt = 0;

	while (*cmd != exitcmd) // цикл обработки команд консоли и подключений
	{
		switch (*cmd)
		{
		case startcmd:
			*cmd = getcommand; // возобновить подключение клиентов
			squirt = AS_SQUIRT; //#define AS_SQUIRT 10
			break;
		case stopcmd:
			squirt = 0;
			break;
		};
		
		if (*cmd == waitcmd)
		{
			if (serverStats.currentConnections == 0)
				*cmd = getcommand; // возобновить подключение клиентов
			SleepEx(0, TRUE);
			continue;
		}

		if (AcceptCycle(squirt)) //цикл запрос/подключение (accept)
		{
			*cmd = getcommand;
			//.... запуск потока EchoServer.......................
			//.... установка ожидающего таймера для процесса EchoServer ...
		}
		else SleepEx(0, TRUE); // выполнить асинхронные процедуры
	};
};

bool AcceptCycle(int squirt)
{
	bool rc = false;
	Contact c(Contact::ACCEPT, "EchoServer");
	c.procedureHandlerThread = serverParams.procedureHandlerThread;

	while (squirt-- > 0 && rc == false)
	{
		if ((c.s = accept(sS, (sockaddr*)&c.prms, &c.lprms)) == INVALID_SOCKET)
		{
			if (WSAGetLastError() != WSAEWOULDBLOCK)
				throw SetErrorMsgText("accept:", WSAGetLastError());
		}
		else
		{
			rc = true; // подключился
			InterlockedIncrement(&serverStats.connectionsOverTime);
			InterlockedIncrement(&serverStats.currentConnections);

			HANDLE event = OpenEvent(EVENT_ALL_ACCESS, TRUE, L"ClientAccept");
			if (!event)
				throw L"AcceptThread-OpenEvent";

			EnterCriticalSection(&listCS);
			ListContact.push_front(c);
			LeaveCriticalSection(&listCS);

			SetEvent(event);
			Sleep(100);
			ResetEvent(event);
		}
	}
	return rc;
};

// ------------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------------

DWORD WINAPI DispatchServerThread(LPVOID command_param)
{
	auto command = static_cast<CMDCommand*>(command_param);
	auto serveClient = reinterpret_cast<HANDLE(*)(const char*, LPVOID)>(GetProcAddress(serverParams.hLib, "ServeClient"));

	while (*command != exitcmd)
	{
		Sleep(50);
		if (WaitForSingleObject(OpenEvent(EVENT_ALL_ACCESS, TRUE, L"ClientAccept"), 5000) == WAIT_OBJECT_0)
		{
			for (auto& contact : ListContact)
			{
				try
				{
					if (contact.type != Contact::TE::ACCEPT) continue;

					auto clientSock = contact.s;

					u_long nonblk;
					if (ioctlsocket(clientSock, FIONBIO, &(nonblk = 0)) == SOCKET_ERROR)
						throw GetMessageError(WSAGetLastError());

					char buffer[1024];
					int result = recv(clientSock, buffer, sizeof(buffer), NULL);

					if (result == SOCKET_ERROR) {
						// ошибка получения данных
						std::cerr << "recv failed: " << result << "\n";
						contact.sthread = Contact::ST::ABORT;
					}
					else if (result == 0) {
						// соединение закрыто клиентом
						contact.sthread = Contact::ST::ABORT;
					}
					else {
						buffer[result] = '\0';
						std::cout << "DispatchServer: " << buffer << endl;

						auto res = CommandReducer(string(buffer), &contact, serveClient);
					}
				}
				catch (string str)
				{
					std::cout << str << endl;
					InterlockedIncrement(&serverStats.denialsOverTime);
				}
			}
		}
	}

	std::cout << "DispatchServer: closed!\n";

	return 0;
}

void PopulateClientWithProps(Contact* contact)
{
	contact->type = Contact::TE::CONTACT;
	contact->sthread = Contact::ST::WORK;
	// client->procedureHandlerThread = serverParams.procedureHandlerThread;
}

string CommandReducer(string msg, Contact* contact, void* (*serveClient)(const char*, LPVOID))
{
	if (msg == "ECHO")
	{
		PopulateClientWithProps(contact);
		
		if (send(contact->s, msg.c_str(), msg.size(), 0) < 0)
		{
			printf("Send failed\n");
		}

		serveClient(msg.c_str(), contact);
		return "ECHO";
	}
	if (msg == "TIME")
	{
		PopulateClientWithProps(contact);
		
		if (send(contact->s, msg.c_str(), msg.size(), 0) < 0)
		{
			printf("Send failed\n");
		}

		serveClient(msg.c_str(), contact);
		return "TIME";

	}

	if (msg == "RAND")
	{
		PopulateClientWithProps(contact);
		
		if (send(contact->s, msg.c_str(), msg.size(), 0) < 0)
		{
			printf("Send failed\n");
		}

		serveClient(msg.c_str(), contact);
		return "RAND";
	}

	msg = "Command undefined";
	if (send(contact->s, msg.c_str(), msg.size(), 0) < 0)
	{
		printf("Send failed\n");
	}

	contact->type = Contact::TE::CONTACT;
	contact->sthread = Contact::ST::ABORT;
	closesocket(contact->s);

	return "Command undefined";
}

// ------------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------------

DWORD WINAPI GarbageCollectorThread(LPVOID command_param)
{
	auto command = static_cast<CMDCommand*>(command_param);
	list<Contact> newList;
	bool changeList = false;

	while (*command != exitcmd)
	{
		Sleep(5000);

		EnterCriticalSection(&listCS);

		for (const auto& contact : ListContact)
		{
			if (contact.sthread == Contact::ST::ABORT || contact.sthread == Contact::ST::FINISH || contact.sthread == Contact::ST::TIMEOUT)
			{
				closesocket(contact.s);
				changeList = true;
				InterlockedDecrement(&serverStats.currentConnections);
			}
			else
			{
				newList.push_back(contact);
			}
		}

		if (changeList)
		{
			ListContact = newList;
		}

		LeaveCriticalSection(&listCS);

		newList.clear();
		changeList = false;

		

		if ((*command == CMDCommand::waitcmd) && (serverStats.currentConnections == 0))
		{
			*command = CMDCommand::getcommand;
		}
		if ((*command == CMDCommand::shutdowncmd) && (serverStats.currentConnections == 0))
		{
			*command = CMDCommand::exitcmd;
		}
	}

	cout << "GarbageCollector: closed!\n";
	return 0;
}

// ------------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------------

CMDCommand ParseCommand(const char* cstr)
{
	const auto str = string(cstr);
	if (str == "START")
		return startcmd;
	if (str == "STOP")
		return stopcmd;
	if (str == "EXIT")
		return exitcmd;
	if (str == "WAIT")
		return waitcmd;
	if (str == "SHUTDOWN")
		return shutdowncmd;
	if (str == "STATS")
		return statscmd;
	throw exception("parse failed");
}

string ExecuteCommand(CMDCommand cmd, CMDCommand* global_command)
{
	const char* successfull_return = "Command executed successfully";
	string response;
	string stats;

	if (*global_command == cmd)
	{
		return "Command is already being executed";
	}

	switch (cmd)
	{
	case startcmd:
	case stopcmd:
	case shutdowncmd:
	case waitcmd:
	case exitcmd:
		*global_command = cmd;
		return successfull_return;
	case statscmd:
		stats += "\n~~~~~~~~~~~~\n";
		stats += "Connections over time:\t" + to_string(serverStats.connectionsOverTime) + "\n";
		stats += "Current connections  :\t" + to_string(serverStats.currentConnections) + "\n";
		stats += "Denials over time    :\t" + to_string(serverStats.denialsOverTime) + "\n";
		stats += "~~~~~~~~~~~~\n";
		return stats;
	default:
		return "Wrong command";
	}


}

DWORD WINAPI ConsolePipeThread(LPVOID command_param)
{
	HANDLE _handle;
	char buf[128];
	DWORD lBuf;
	DWORD bufSize = 128;

	if ((_handle = CreateNamedPipe(L"\\\\.\\pipe\\NordStream2", // Создание именованного канала, возвращает дескриптор(HANDEL) канала 
		PIPE_ACCESS_DUPLEX, // атрибуты канала 
		PIPE_TYPE_MESSAGE | PIPE_WAIT | PIPE_READMODE_MESSAGE, // режимы передачи данных
		1, NULL, NULL,
		INFINITE, NULL)) == INVALID_HANDLE_VALUE)
	{
		throw SetPipeError("Create: ", GetLastError());
	}

	if (!ConnectNamedPipe(_handle, NULL))
		throw SetPipeError("Connect: ", GetLastError());

	auto global_command = static_cast<CMDCommand*>(command_param);
	while (*global_command != exitcmd)
	{
		Sleep(1000);
		try
		{
			if (!ReadFile(_handle, buf, sizeof(buf), &lBuf, NULL))
				throw SetPipeError("ReadFile: ", GetLastError());

			string msg = string(buf);
			string response = "Wrong command";

			if (msg.empty())
			{
				DisconnectNamedPipe(_handle);
				ConnectNamedPipe(_handle, NULL);
				continue;
			}

			try
			{
				const auto cmd = ParseCommand(msg.c_str());
				response = ExecuteCommand(cmd, global_command);
			}
			catch (...)
			{
				response = "Wrong command";
			}

			if (!WriteFile(_handle, response.c_str(), response.size(), &lBuf, NULL))
				throw SetPipeError("WriteFile: ", GetLastError());
		}
		catch (string excep)
		{
			const auto err_code = GetLastError();
			constexpr int client_disconnected = 109;
			if (err_code == client_disconnected)
			{
				DisconnectNamedPipe(_handle);
				ConnectNamedPipe(_handle, NULL);
			}
		}
	}

	CloseHandle(_handle);
	cout << "ConsolePipe: closed!\n";

	return 0;
}

// ------------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------------

bool GetRequestFromClient(SOCKET udp_s, const char* name, short port, struct sockaddr* from, int* flen)
{
	try
	{
		SOCKADDR_IN clnt1; // параметры сокета клиента
		std::memset(&clnt1, 0, sizeof(clnt1)); // обнулить память
		int lc = sizeof(clnt1);

		char ibuf[50]; //буфер ввода
		int lb = 0; //количество принятых байт

		if ((lb = recvfrom(udp_s, ibuf, sizeof(ibuf), NULL,
			(sockaddr*)&clnt1, &lc)) == SOCKET_ERROR)
		{
			if (WSAGetLastError() == WSAEWOULDBLOCK)
			{
				return false;
			}
			else
			{
				throw SetErrorMsgText("recv:", WSAGetLastError());
			}
		}

		string messagebuf = ibuf;
		string newName = name;

		if (messagebuf == newName)
		{
			*from = *((sockaddr*)&clnt1);
			*flen = lc;

			return true;
		}
	}
	catch (string errorMsgText)
	{
		return false;
	}
}

bool PutAnswerToClient(SOCKET udp_s, const char* name, string port, struct sockaddr* to, int* lto)
{
	try
	{
		int lobuf = 0; //количество отправленных 
		if ((lobuf = sendto(udp_s, port.c_str(), port.size(), NULL,
			to, *lto)) == SOCKET_ERROR)
			throw SetErrorMsgText("sendto:", WSAGetLastError());
	}
	catch (string errorMsgText)
	{
		return false;
	}
}

DWORD WINAPI ResponseServerThread(LPVOID command_param)
{
	auto command = static_cast<CMDCommand*>(command_param);

	try
	{
		WSADATA wsaData;
		if (WSAStartup(MAKEWORD(2, 0), &wsaData) != 0)
			throw SetErrorMsgText("WSASturtup: ", WSAGetLastError());

		SOCKET udp_s;
		if ((udp_s = socket(AF_INET, SOCK_DGRAM, NULL)) == INVALID_SOCKET)
			throw SetErrorMsgText("socket:", WSAGetLastError());

		SOCKADDR_IN serv; // параметры сокета sS
		serv.sin_family = AF_INET; // используется IP-адресация 
		serv.sin_port = htons(serverParams.responsePort); // порт 2000 
		serv.sin_addr.s_addr = INADDR_ANY; // адрес сервера

		if (bind(udp_s, (LPSOCKADDR)&serv, sizeof(serv)) == SOCKET_ERROR)
			throw SetErrorMsgText("bind:", WSAGetLastError());

		u_long nonblk;
		if (ioctlsocket(udp_s, FIONBIO, &(nonblk = 1)) == SOCKET_ERROR)
			throw GetMessageError(WSAGetLastError());

		SOCKADDR_IN clnt; // параметры сокета клиента
		memset(&clnt, 0, sizeof(clnt)); // обнулить память
		int lc = sizeof(clnt);

		while (*command != exitcmd)
		{
			Sleep(500);
			if (*command == stopcmd || *command == shutdowncmd || *command == waitcmd)
			{
				continue;
			}
			SOCKADDR_IN addr{};
			if (GetRequestFromClient(udp_s, serverParams.serviceName.c_str(), serverParams.responsePort, (sockaddr*)&clnt, &lc))
			{
				const auto acceptPort = to_string(serverParams.port);
				PutAnswerToClient(udp_s, serverParams.serviceName.c_str(), std::to_string(serverParams.port), (sockaddr*)&clnt, &lc);
			}
		}
	}
	catch (string excep)
	{
		cout << "ResponseServer: " << excep << endl;
	}

	cout << "ResponseServer: closed!\n";
	return 0;
}

// ------------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------------
// ------------------------------------------------------------------------------------------

int main(int argc, char* argv[])
{
	setlocale(LC_ALL, "Ru");
	ReduceArgs(argc, argv);

	serverParams.hLib = LoadLibraryA(serverParams.dllName.c_str());
	if (serverParams.hLib == 0)
		throw ("Libary not found!");

	InitializeCriticalSection(&listCS);

	void* global_command_voidptr = &global_variance_of_cmd_command;
	auto hAcceptServer = CreateThread(NULL, NULL, AcceptServerThread, global_command_voidptr, NULL, NULL);
	auto hDispatcherServer = CreateThread(NULL, NULL, DispatchServerThread, global_command_voidptr, NULL, NULL);
	auto hGarbageCollectorThread = CreateThread(NULL, NULL, GarbageCollectorThread, global_command_voidptr, NULL, NULL);
	auto hConsolePipeThread = CreateThread(NULL, NULL, ConsolePipeThread, global_command_voidptr, NULL, NULL);
	auto hResponseServerThread = CreateThread(NULL, NULL, ResponseServerThread, global_command_voidptr, NULL, NULL);
	serverParams.procedureHandlerThread = hAcceptServer;

	SetThreadPriority(hAcceptServer, THREAD_PRIORITY_NORMAL);
	SetThreadPriority(hResponseServerThread, THREAD_PRIORITY_NORMAL);
	SetThreadPriority(hDispatcherServer, THREAD_PRIORITY_ABOVE_NORMAL);
	SetThreadPriority(hConsolePipeThread, THREAD_PRIORITY_NORMAL);
	SetThreadPriority(hGarbageCollectorThread, THREAD_PRIORITY_NORMAL);

	WaitForSingleObject(hAcceptServer, INFINITE);
	WaitForSingleObject(hDispatcherServer, INFINITE);
	WaitForSingleObject(hGarbageCollectorThread, INFINITE);
	WaitForSingleObject(hConsolePipeThread, INFINITE);

	CloseHandle(hAcceptServer);
	CloseHandle(hDispatcherServer);
	CloseHandle(hGarbageCollectorThread);
	CloseHandle(hResponseServerThread);
	TerminateProcess(hConsolePipeThread, 0);

	return 0;
}