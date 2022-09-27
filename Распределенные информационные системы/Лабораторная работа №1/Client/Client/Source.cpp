#include <WinSock2.h>
#include <WS2tcpip.h>
#pragma comment(lib, "WS2_32.lib")
#include <iostream>
#include <list>
#include <string>
using namespace std;

string GetErrorMsgText(int code) // cформировать текст ошибки
{
	string msgText;
	switch (code) // проверка кода возврата 
	{
	case WSAEINTR: msgText = "WSAEINTR"; break;
	case WSAEACCES: msgText = "WSAEACCES"; break;
		//..........коды WSAGetLastError ..........................
	case WSASYSCALLFAILURE: msgText = "WSASYSCALLFAILURE"; break;
	default: msgText = "***ERROR***"; break;
	};
	return msgText;
};

string SetErrorMsgText(string msgText, int code)
{
	return msgText + GetErrorMsgText(code);
};

string GetServer(char* call, short port, struct sockaddr* from, int* flen)
{
	try
	{
		SOCKET cC;

		if ((cC = socket(AF_INET, SOCK_DGRAM, NULL)) == INVALID_SOCKET)
			throw SetErrorMsgText("socket:", WSAGetLastError());

		int optval = 1;
		if (setsockopt(cC, SOL_SOCKET, SO_BROADCAST,
			(char*)&optval, sizeof(int)) == SOCKET_ERROR)
			throw SetErrorMsgText("opt:", WSAGetLastError());

		SOCKADDR_IN sS; // параметры сокета sS
		sS.sin_family = AF_INET; // используется IP-адресация 
		sS.sin_port = htons(port); // порт 2000
		sS.sin_addr.s_addr = INADDR_BROADCAST; // всем
		string stringCall = call;
		int sendlen = 0;

		cout << "Search server" << endl;
		if ((sendlen = sendto(cC, stringCall.c_str(), sizeof(stringCall), NULL,
			(sockaddr*)&sS, sizeof(sS))) == SOCKET_ERROR)
			throw SetErrorMsgText("sendto:", WSAGetLastError());

		char ibuf[50]; //буфер ввода 
		int lb = 0; //количество принятых байт
		int lc = sizeof(sS);
		if ((lb = recvfrom(cC, ibuf, sizeof(ibuf), NULL,
			(sockaddr*)&sS, &lc)) == SOCKET_ERROR)
		{
			if (WSAGetLastError() == WSAETIMEDOUT)
			{
				return nullptr;
			}
			else
			{
				throw SetErrorMsgText("recv:", WSAGetLastError());
			}
		}

		ibuf[lb] = '\0';
		string recvNameServ = ibuf;
		closesocket(cC);

		return recvNameServ;
	}
	catch (string errorMsgText)
	{
		cout << endl << errorMsgText;
	}
}

int main()
{
	setlocale(LC_ALL, "Ru");
	WSADATA wsaData;

	if (WSAStartup(MAKEWORD(2, 0), &wsaData) != 0)
		throw SetErrorMsgText("WSASturtup: ", WSAGetLastError());

	struct sockaddr_in from;
	int flen = 0;

	string str = "2000";

	std::cout << "Server port: " << str << endl;

	int port = stoi(str);

	SOCKET sock;
	SOCKADDR_IN addr_in;
	std::string msg = "ECHO";
	char buffer[1024];

	while (true)
	{
		if ((sock = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
			printf("\n Socket creation error \n");
			return -1;
		}

		addr_in.sin_family = AF_INET;
		addr_in.sin_port = htons(port);
		if (inet_pton(AF_INET, "127.0.0.1", &addr_in.sin_addr) <= 0)
		{
			printf("\nInvalid address/ Address not supported \n");
			return -1;
		}

		if (connect(sock, (struct sockaddr*) & addr_in, sizeof(addr_in)) < 0)
		{
			printf("\nConnection Failed \n");
			return -1;
		}

		cout << "Insert operation: ";
		cin >> msg;

		if (msg == "ECHO")
		{
			if (send(sock, msg.c_str(), msg.size(), 0) < 0)
			{
				printf("Send failed\n");
			}

			int result = recv(sock, buffer, sizeof(buffer), NULL);

			if (result == SOCKET_ERROR) {
				// ошибка получения данных
				std::cerr << "recv failed: " << result << "\n";
				closesocket(sock);
			}
			else if (result == 0) {
				// соединение закрыто клиентом
				std::cerr << "connection closed...\n";
			}
			else {
				buffer[result] = '\0';
				std::cout << "Server send: " << buffer << "\n";
			}

			while (true)
			{
				cout << "Print line: ";
				cin >> msg;

				if (msg == "exit") msg = "";

				if (send(sock, msg.c_str(), msg.size(), 0) < 0)
				{
					printf("Send failed\n");
					break;
				}

				if (msg == "") break;

				int result = recv(sock, buffer, sizeof(buffer), NULL);

				if (result == SOCKET_ERROR) {
					// ошибка получения данных
					std::cerr << "recv failed: " << result << "\n";
					closesocket(sock);
					break;
				}
				else if (result == 0) {
					// соединение закрыто клиентом
					std::cerr << "connection closed...\n";
					break;
				}
				else {
					buffer[msg.size()] = '\0';
					std::cout << buffer << "\n";
				}
			}
		}
		else if (msg == "TIME")
		{
			if (send(sock, msg.c_str(), msg.size(), 0) < 0)
			{
				printf("Send failed\n");
			}

			int result = recv(sock, buffer, sizeof(buffer), NULL);

			if (result == SOCKET_ERROR) {
				// ошибка получения данных
				std::cerr << "recv failed: " << result << "\n";
				closesocket(sock);
			}
			else if (result == 0) {
				// соединение закрыто клиентом
				std::cerr << "connection closed...\n";
			}
			else {
				buffer[msg.size()] = '\0';
				std::cout << "Server send: " << buffer << "\n";
			}

			while (true)
			{
				cout << "Print line: ";
				cin >> msg;

				if (msg == "exit") msg = "";

				if (send(sock, msg.c_str(), msg.size(), 0) < 0)
				{
					printf("Send failed\n");
					break;
				}

				if (msg == "") break;

				int result = recv(sock, buffer, sizeof(buffer), NULL);

				if (result == SOCKET_ERROR) {
					// ошибка получения данных
					std::cerr << "recv failed: " << result << "\n";
					closesocket(sock);
					break;
				}
				else if (result == 0) {
					// соединение закрыто клиентом
					std::cerr << "connection closed...\n";
					break;
				}
				else {
					if (result >= 1024) result--;
					buffer[result] = '\0';
					std::cout << buffer << "\n";
				}
			}
		}
		else if (msg == "RAND")
		{
			if (send(sock, msg.c_str(), msg.size(), 0) < 0)
			{
				printf("Send failed\n");
			}

			int result = recv(sock, buffer, sizeof(buffer), NULL);

			if (result == SOCKET_ERROR) {
				// ошибка получения данных
				std::cerr << "recv failed: " << result << "\n";
				closesocket(sock);
			}
			else if (result == 0) {
				// соединение закрыто клиентом
				std::cerr << "connection closed...\n";
			}
			else {
				buffer[msg.size()] = '\0';
				std::cout << "Server send: " << buffer << "\n";
			}

			while (true)
			{
				cout << "Print line: ";
				cin >> msg;

				if (msg == "exit") msg = "";

				if (send(sock, msg.c_str(), msg.size(), 0) < 0)
				{
					printf("Send failed\n");
					break;
				}

				if (msg == "") break;

				int result = recv(sock, buffer, sizeof(buffer), NULL);

				if (result == SOCKET_ERROR) {
					// ошибка получения данных
					std::cerr << "recv failed: " << result << "\n";
					closesocket(sock);
					break;
				}
				else if (result == 0) {
					// соединение закрыто клиентом
					std::cerr << "connection closed...\n";
					break;
				}
				else {
					if (result >= 1024) result--;
					buffer[result] = '\0';
					std::cout << buffer << "\n";
				}
			}
		}
		else {
			cout << "Not command" << endl;
			closesocket(sock);
			break;
		}

		closesocket(sock);
	}

	closesocket(sock);
	WSACleanup();

	return 0;
}