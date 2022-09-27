#define _WINSOCK_DEPRECATED_NO_WARNINGS
#define _CRT_SECURE_NO_WARNINGS 

#include <string>
#include <list>
#include <time.h>
#include <iostream>
#include "ErrorHandling.h"
#include "Winsock2.h"                
#pragma comment(lib, "WS2_32.lib")  

using namespace std;



HANDLE hAcceptServer;
HANDLE hDispatchServer;
HANDLE hConsolePipe;
HANDLE hGarbageCleaner;
HANDLE hResponseServer;

int serverPort;
char dllName[50];
char namedPipeName[20];

SOCKADDR_IN serv;
SOCKADDR_IN clnt;
SOCKET sS;
SOCKET cS;

volatile long failed = 0;
volatile long finished = 0;
volatile long currentActiveConnections = 0;
volatile long countConnections = 0;

HANDLE(*ts)(char*, LPVOID);

CRITICAL_SECTION scListContact;
HANDLE hClientConnectedEvent = CreateEvent(NULL,
	FALSE, //�������������� �����
	FALSE,
	L"ClientConnected");;

enum TalkersCommand {
	NoCmd, START, STOP, EXIT, STATISTICS, WAIT, SHUTDOWN, GETCOMMAND
};
volatile TalkersCommand  previousCommand = GETCOMMAND;      // ���������� ������� ������� 

struct Contact         // ������� ������ �����������       
{
	enum TE {               // ���������  ������� �����������  
		EMPTY,              // ������ ������� ������ ����������� 
		ACCEPT,             // ��������� (accept), �� �� �������������
		CONTACT             // ������� �������������� �������  
	}    type;     // ��� �������� ������ ����������� 
	enum ST {               // ��������� �������������� �������  
		WORK,               // ���� ����� ������� � ��������
		ABORT,              // ������������� ������ ���������� �� ��������� 
		TIMEOUT,            // ������������� ������ ���������� �� ������� 
		FINISH              // ������������� ������ ����������  ��������� 
	}      sthread; // ���������  �������������� ������� (������)

	SOCKET      s;         // ����� ��� ������ ������� � ��������
	SOCKADDR_IN prms;      // ���������  ������ 
	int         lprms;     // ����� prms 
	HANDLE      hthread;   // handle ������ (��� ��������) 
	HANDLE      htimer;    // handle �������
	HANDLE		serverHThtead;// handle �������������� ������� ������� � ����������� ����� ���������
	bool        TimerOff = false;  // ����� ������������ �������

	char msg[50];           // ��������� 
	char srvname[15];       //  ������������ �������������� ������� 

	Contact(TE t = EMPTY, const char* namesrv = "") // ����������� 
	{
		ZeroMemory(&prms, sizeof(SOCKADDR_IN));
		lprms = sizeof(SOCKADDR_IN);
		type = t;
		strcpy(srvname, namesrv);
		msg[0] = 0x00;
	};

	void SetST(ST sth, const char* m = "")
	{
		sthread = sth;
		strcpy(msg, m);
	}
};

typedef list<Contact> ListContact;
ListContact contacts;
SOCKET sSUDP;

bool PutAnswerToClient(char* name, sockaddr* to, int* lto) {
	if ((sendto(sSUDP, name, sizeof(name) + 1, NULL, to, *lto)) == SOCKET_ERROR)
		throw  SetErrorMsgText("sendto:", WSAGetLastError());
	return false;
}


bool GetRequestFromClient(char* name, short port, SOCKADDR_IN* from, int* flen)
{
	SOCKADDR_IN clnt;               // ���������  ������ �������
	int lc = sizeof(clnt);
	ZeroMemory(&clnt, lc);   // �������� ������
	char ibuf[500];                  //����� ����� 
	int  lb = 0;                    //���������� �������� ����
	int optval = 1;
	while (true) {
		if ((lb = recvfrom(sSUDP, ibuf, sizeof(ibuf), NULL, (sockaddr*)&clnt, &lc)) == SOCKET_ERROR)
			return false;
		if (!strcmp(name, ibuf)) {
			*from = clnt;
			*flen = lc;
			return false;
		}
		else
		{
			break;
		}
	}
	return true;
}


DWORD WINAPI ResponseServer(LPVOID pPrm)
{
	DWORD rc = 0;
	WSADATA wsaData;
	SOCKADDR_IN serv1;                     // ���������  ������ sS
	if (WSAStartup(MAKEWORD(2, 0), &wsaData) != 0)
		throw  SetErrorMsgText("Startup:", WSAGetLastError());
	if ((sSUDP = socket(AF_INET, SOCK_DGRAM, NULL)) == INVALID_SOCKET)
		throw  SetErrorMsgText("socket:", WSAGetLastError());

	serv1.sin_family = AF_INET;           // ������������ IP-���������  
	serv1.sin_port = htons(serverPort);          // ���� 2000
	serv1.sin_addr.s_addr = INADDR_ANY; //inet_addr("192.168.226.109");   // ����� ����������� IP-����� 

	if (bind(sSUDP, (LPSOCKADDR)&serv1, sizeof(serv1)) == SOCKET_ERROR)
		throw  SetErrorMsgText("bind:", WSAGetLastError());

	char name[50] = "Hello";

	SOCKADDR_IN from;               // ���������  ������ �������
	int lc = sizeof(from);
	ZeroMemory(&from, lc);
	int numberOfClients = 0;
	while (*(TalkersCommand*)pPrm != EXIT)
	{
		try
		{
			if (GetRequestFromClient(name, serverPort, &from, &lc))
			{
				printf("\nClient: IP: %s\nPort: %d\n", inet_ntoa(from.sin_addr), htons(from.sin_port));
				PutAnswerToClient(name, (sockaddr*)&from, &lc);
			}
		}
		catch (string errorMsgText)
		{
			printf("\n%s", errorMsgText.c_str());
		}
	}
	if (closesocket(sSUDP) == SOCKET_ERROR)
		throw  SetErrorMsgText("closesocket:", WSAGetLastError());
	ExitThread(rc);
}


void CALLBACK ASWTimer(LPVOID Lprm, DWORD, DWORD)
{
	Contact* client = (Contact*)Lprm; //����������� ���������� ��������
	EnterCriticalSection(&scListContact); //������ � ����������� ������
	client->TimerOff = true; 	//������ ����� ������������ �������
	client->sthread = Contact::TIMEOUT;
	LeaveCriticalSection(&scListContact); 	//������� �� ����������� ������
	SYSTEMTIME stt;
	// �������� ������� ����� � ����
	GetLocalTime(&stt);
	// ������� ���������
	printf("%d.%d.%d %d:%02d Timeout \n", stt.wDay, stt.wMonth, stt.wYear, stt.wHour, stt.wMinute);
	std::cout << client->srvname << ";" << std::endl;
}


bool AcceptCycle(int countConnected)
{
	bool rc = false;
	Contact c(Contact::ACCEPT, "AcceptServer");

	while (countConnected-- > 0 && rc == false)
	{
		if ((c.s = accept(sS, (sockaddr*)&c.prms, &c.lprms)) == INVALID_SOCKET)
		{
			if (WSAGetLastError() != WSAEWOULDBLOCK)
				throw  SetErrorMsgText("Accept: ", WSAGetLastError());
		}
		else
		{
			cout << "Client connected." << endl;
			cout << "IP: " <<inet_ntoa(c.prms.sin_addr) << "\nPort: " << htons(c.prms.sin_port) << endl << endl << endl;

			InterlockedIncrement(&countConnections);
			rc = true;
			EnterCriticalSection(&scListContact);
			contacts.push_front(c);
			LeaveCriticalSection(&scListContact);
			SetEvent(hClientConnectedEvent);
		}
	}
	return rc;
};


static void WaitClients()
{
	bool ListEmpty = false;
	while (!ListEmpty)
	{
		EnterCriticalSection(&scListContact);
		ListEmpty = contacts.empty();
		LeaveCriticalSection(&scListContact); 
		SleepEx(0, TRUE);
	}
}


void CommandsCycle(TalkersCommand& cmd)      // ���� ��������� ������
{
	int countConnected = 0;
	while (cmd != EXIT)     // ���� ��������� ������ ������� � �����������
	{
		switch (cmd)
		{
		case START: cmd = GETCOMMAND; // ����������� ����������� ��������
			countConnected = 10;
			break;
		case STOP:  cmd = GETCOMMAND; // ���������� ����������� ��������   
			countConnected = 0;
			break;
		case WAIT:  //���������� �������, � ����� ��������� ����������� ����� ��������
			WaitClients();
			cmd = GETCOMMAND;
			countConnected = 10;
			break;
		case SHUTDOWN:  //���������� �������, � ����� ��������� ������
			WaitClients();
			cmd = EXIT;
			break;
		};
		if (AcceptCycle(countConnected))   //���� ������/����������� (accept) 
		{
			cmd = GETCOMMAND;
		}
		else SleepEx(0, TRUE);    // ��������� ����������� ��������� 
	};
};


DWORD WINAPI AcceptServer(LPVOID pPrm)    // �������� 
{
	DWORD rc = 0;    // ��� �������� 
	WSADATA wsaData;
	u_long nonblk = 1; // ������� ����������� � ������ -> nonblocking �����
	int lclnt = sizeof(clnt);    // ������ SOCKADDR_IN

	serv.sin_family = AF_INET;           // ������������ IP-���������  
	serv.sin_port = htons(serverPort);
	serv.sin_addr.s_addr = INADDR_ANY;   // ����� ����������� IP-����� 

	try
	{
		if (WSAStartup(MAKEWORD(2, 0), &wsaData) != 0)
			throw SetErrorMsgText("Startup: ", WSAGetLastError());

		if ((sS = socket(AF_INET, SOCK_STREAM, NULL)) == INVALID_SOCKET)
			throw  SetErrorMsgText("Socket: ", WSAGetLastError());

		if (bind(sS, (LPSOCKADDR)&serv, sizeof(serv)) == SOCKET_ERROR)
			throw  SetErrorMsgText("Bind: ", WSAGetLastError());

		if (listen(sS, SOMAXCONN) == SOCKET_ERROR)
			throw SetErrorMsgText("Listen: ", WSAGetLastError());

		if (ioctlsocket(sS, FIONBIO, &nonblk) == SOCKET_ERROR)
			throw SetErrorMsgText("Ioctlsocket: ", WSAGetLastError());

		CommandsCycle(*((TalkersCommand*)pPrm));

		
		if (closesocket(sS) == SOCKET_ERROR)
			throw  SetErrorMsgText("�losesocket:", WSAGetLastError());
		
		if (WSACleanup() == SOCKET_ERROR)
			throw  SetErrorMsgText("Cleanup:", WSAGetLastError());
	}
	catch (string errorMsgText)
	{
		cout << endl << "WSAGetLastError: " << errorMsgText;
	}
	
	ExitThread(rc);
}


DWORD WINAPI DispatchServer(LPVOID pPrm)    // �������� 
{
	DWORD rc = 0;    // ��� �������� 
	TalkersCommand& command = *(TalkersCommand*)pPrm;
	LARGE_INTEGER Li;

	while (command != EXIT)
	{
		if (command != STOP)
		{
			WaitForSingleObject(hClientConnectedEvent, INFINITE);// ���� ���� ����������� ������ - � acceptCycle ������� �������� � ���������� ���������
															 
			ResetEvent(hClientConnectedEvent);
			EnterCriticalSection(&scListContact);
			for (auto i = contacts.begin(); i != contacts.end(); i++) {
				if (i->type == i->ACCEPT) {
					u_long nonblk = 0;
					if (ioctlsocket(i->s, FIONBIO, &nonblk) == SOCKET_ERROR)
						throw SetErrorMsgText("ioctlsocket:", WSAGetLastError());

					char serviceType[5];// = "Echo", "Time", "Rand"
					clock_t start = clock();
					recv(i->s, serviceType, sizeof(serviceType), NULL);
					strcpy(i->msg, serviceType);

					clock_t delta = clock() - start;
					if (delta > 10000) {
						puts("Timeout");
						i->sthread = i->TIMEOUT;
						if ((send(i->s, "Timeout", strlen("Timeout") + 1, NULL)) == SOCKET_ERROR)
							throw  SetErrorMsgText("send:", WSAGetLastError());
						if (closesocket(i->s) == SOCKET_ERROR)
							throw  SetErrorMsgText("closesocket:", WSAGetLastError());
						i->type = i->EMPTY;
						InterlockedDecrement(&currentActiveConnections);
					}
					else if (delta <= 10000) {
						if (!strcmp(i->msg, "Echo") && !strcmp(i->msg, "Time") && !strcmp(i->msg, "Rand")) {
							if ((send(i->s, "ErrorInquiry", strlen("ErrorInquiry") + 1, NULL)) == SOCKET_ERROR)
								throw  SetErrorMsgText("send:", WSAGetLastError());
							i->sthread = i->ABORT;
							i->type = i->EMPTY;
							InterlockedDecrement(&currentActiveConnections);
							if (closesocket(i->s) == SOCKET_ERROR)
								throw  SetErrorMsgText("closesocket:", WSAGetLastError());
						}
						else {
							InterlockedIncrement(&currentActiveConnections);
							i->type = i->CONTACT;
							i->hthread = hAcceptServer;
							i->serverHThtead = ts(serviceType, (LPVOID) & (*i));
							i->htimer = CreateWaitableTimer(NULL, FALSE, NULL);
							Li.QuadPart = -(1000000 * 60);
							SetWaitableTimer(i->htimer, &Li, 0, ASWTimer, (LPVOID) & (*i), FALSE);
							SleepEx(0, TRUE);
						}
					}
				}
			}
			LeaveCriticalSection(&scListContact);
		}
	}
	ExitThread(rc); 
}


DWORD WINAPI GarbageCleaner(LPVOID pPrm)
{
	DWORD rc = 0;
	TalkersCommand& command = *(TalkersCommand*)pPrm;
	try
	{
		while (*((TalkersCommand*)pPrm) != EXIT)
		{
			if (contacts.size() != 0) {
				for (auto i = contacts.begin(); i != contacts.end();)
				{
					if (i->sthread == Contact::FINISH || i->sthread == Contact::TIMEOUT || i->sthread == Contact::ABORT || i->type == Contact::EMPTY)
					{
						cout << "Disconnected: " << inet_ntoa(i->prms.sin_addr) << "\n" << htons(i->prms.sin_port) << endl << endl << endl;
						EnterCriticalSection(&scListContact);
						if (i->type == Contact::EMPTY) InterlockedIncrement(&failed);
						else
						{
							HANDLE nulX;
							if (i->sthread == Contact::FINISH) InterlockedIncrement(&finished);
							if (i->sthread == Contact::TIMEOUT) InterlockedIncrement(&failed);
							if (i->sthread == Contact::ABORT) InterlockedIncrement(&failed);
						}
						closesocket(i->s);
						i = contacts.erase(i);
						InterlockedDecrement(&currentActiveConnections);
						LeaveCriticalSection(&scListContact);
					}

					else i++;
				}
			}
			Sleep(1000);
		}
	}
	catch (string errorMsgText)
	{
		std::cout << errorMsgText << endl;
	}
	ExitThread(rc); 
}


TalkersCommand set_param(char* param) {
	if (!strcmp(param, "start")) return START;
	if (!strcmp(param, "stop")) return STOP;
	if (!strcmp(param, "exit")) return EXIT;
	if (!strcmp(param, "wait")) return WAIT;
	if (!strcmp(param, "shutdown")) return SHUTDOWN;
	if (!strcmp(param, "statistics")) return STATISTICS;
	if (!strcmp(param, "getcommand")) return GETCOMMAND;

	return NoCmd;
}


DWORD WINAPI ConsolePipe(LPVOID pPrm)
{
	DWORD rc = 0;
	char rbuf[25];
	DWORD dwRead, dwWrite;
	HANDLE hPipe;
	try
	{
		char namedPipeConnectionString[50];
		sprintf(namedPipeConnectionString, "\\\\.\\pipe\\jeka", namedPipeName);
		if ((hPipe = CreateNamedPipeA(namedPipeConnectionString,
			PIPE_ACCESS_DUPLEX,
			PIPE_TYPE_MESSAGE | PIPE_WAIT,
			1, NULL, NULL,
			INFINITE, NULL)) == INVALID_HANDLE_VALUE)
			throw SetErrorMsgText("create: ", GetLastError());

		if (!ConnectNamedPipe(hPipe, NULL))
			throw SetErrorMsgText("connect: ", GetLastError());

		TalkersCommand& param = *((TalkersCommand*)pPrm);
		while (param != EXIT) {
			ConnectNamedPipe(hPipe, NULL);
			while (ReadFile(hPipe, rbuf, sizeof(rbuf), &dwRead, NULL))
			{
				printf("Received message: %s\n", rbuf);
				param = set_param(rbuf);
				if (param == STATISTICS)
				{
					char sendStastistics[200];
					sprintf(sendStastistics, "\n-------STATISTICS-------\n"\
						"Amount connections: %d\n" \
						"Failed: %d\n" \
						"Successful: %d\n" \
						"Connected: %d\n" \
						"---------------------", countConnections, failed, finished, contacts.size());
					WriteFile(hPipe, sendStastistics, sizeof(sendStastistics), &dwWrite, NULL);
				}
				else if (param == STOP)
				{
					SuspendThread(hAcceptServer);
					SuspendThread(hResponseServer);
				}
				else if (param == START)
				{
					try
					{
						ResumeThread(hAcceptServer);
						ResumeThread(hResponseServer);
					}
					catch (string ErrorPipeText)
					{
					}
				}
				else if (param == WAIT)
				{
					SuspendThread(hAcceptServer);
					ResumeThread(hResponseServer);
					while (contacts.size() > 1)
					{

					}
					ResumeThread(hAcceptServer);
				}
				else if (param == SHUTDOWN)
				{
					SuspendThread(hAcceptServer);
					ResumeThread(hResponseServer);
					while (contacts.size() != 0)
					{

					}
					DisconnectNamedPipe(hPipe);
					exit(0);
				}
				else if (param == EXIT)
				{
					puts("------Exit pipe------");
					DisconnectNamedPipe(hPipe);
					exit(0);
				}
				if (param != STATISTICS) {
					if (param != NoCmd) {
						char successMessage[20];
						sprintf(successMessage, "success");
						WriteFile(hPipe, successMessage, strlen(successMessage) + 1, &dwWrite, NULL);
					}
					else {
						char failureMessage[20];
						sprintf(failureMessage, "failure");
						WriteFile(hPipe, failureMessage, strlen(failureMessage) + 1, &dwWrite, NULL);
					}
				}
			}
		}
	}
	catch (string ErrorPipeText)
	{
		printf("\n%s", ErrorPipeText.c_str());
		return -1;
	}

	CloseHandle(hPipe);
	ExitThread(rc);  // ���������� ������ ������
}

int main(int argc, char* argv[]) {
	setlocale(LC_ALL, "");

	if (argc == 2) {
		serverPort = atoi(argv[1]);
	}
	else if (argc == 3)
	{
		serverPort = atoi(argv[1]);
		strcpy(dllName, argv[2]);
	}
	else if (argc == 4)
	{
		serverPort = atoi(argv[1]);
		strcpy(dllName, argv[2]);
		strcpy(namedPipeName, argv[3]);
	}
	else
	{
		serverPort = 2000;
		strcpy(dllName, "ServiceLibrary");
		strcpy(namedPipeName, "jeka");
	}
	cout << "------Server launched------ " << endl;
	cout << "Port: " << serverPort << endl;
	cout << "NamedPipe: " << namedPipeName << endl;
	cout << "DLL: " << dllName << endl;
	cout << "------Waiting for commands------ " << endl;

	HMODULE st = LoadLibraryA(dllName);
	ts = (HANDLE(*)(char*, LPVOID))GetProcAddress(st, "SSS");


	volatile TalkersCommand cmd = START;

	InitializeCriticalSection(&scListContact);

	hAcceptServer = CreateThread(NULL, NULL, AcceptServer, (LPVOID)&cmd, NULL, NULL);
	hDispatchServer = CreateThread(NULL, NULL, DispatchServer, (LPVOID)&cmd, NULL, NULL);
	hGarbageCleaner = CreateThread(NULL, NULL, GarbageCleaner, (LPVOID)&cmd, NULL, NULL);
	hConsolePipe = CreateThread(NULL, NULL, ConsolePipe, (LPVOID)&cmd, NULL, NULL);
	hResponseServer = CreateThread(NULL, NULL, ResponseServer, (LPVOID)&cmd, NULL, NULL);

	SetThreadPriority(hAcceptServer, THREAD_PRIORITY_HIGHEST);
	SetThreadPriority(hDispatchServer, THREAD_PRIORITY_NORMAL);
	SetThreadPriority(hGarbageCleaner, THREAD_PRIORITY_BELOW_NORMAL);
	SetThreadPriority(hConsolePipe, THREAD_PRIORITY_NORMAL);
	SetThreadPriority(hResponseServer, THREAD_PRIORITY_ABOVE_NORMAL);

	WaitForSingleObject(hAcceptServer, INFINITE);
	CloseHandle(hAcceptServer);
	WaitForSingleObject(hDispatchServer, INFINITE);
	CloseHandle(hDispatchServer);
	WaitForSingleObject(hGarbageCleaner, INFINITE);
	CloseHandle(hGarbageCleaner);
	WaitForSingleObject(hConsolePipe, INFINITE);
	CloseHandle(hConsolePipe);
	
	DeleteCriticalSection(&scListContact);

	FreeLibrary(st);

	system("pause");
	return 0;
}