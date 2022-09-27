#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <string>
#include <iostream>
#include <ctime>
#include "ErrorHandling.h"
#include "Winsock2.h" 
#pragma comment(lib, "WS2_32.lib")

using namespace std;

SOCKET cC;

SOCKADDR_IN serv;
SOCKADDR_IN clientConfig;
char name[20] = "Hello";

char ibuf[50] = "server: get "; //буфер вывода
int libuf = 0; //количество отправленных байт
int lobuf = 0; //количество принятых байт
int numOfServers = 0;

bool GetServer(char* call, short port, sockaddr* from, int lfrom) {
	try
	{
		bool notErr = true;
		char buf[50] = "";
		SOCKET GS;
		u_long nonblk = 0;

		if ((GS = socket(AF_INET, SOCK_DGRAM, NULL)) == INVALID_SOCKET)
			throw GetErrorMsgText(WSAGetLastError());

		((SOCKADDR_IN*)from)->sin_family = AF_INET;
		((SOCKADDR_IN*)from)->sin_port = htons(port);
		((SOCKADDR_IN*)from)->sin_addr.s_addr =  INADDR_BROADCAST;


		if (setsockopt(GS, SOL_SOCKET, SO_BROADCAST, (char*)&port, sizeof(int)) == SOCKET_ERROR)
			throw  GetErrorMsgText(WSAGetLastError());

		if ((sendto(GS, call, strlen(call) + 1, NULL, from, lfrom)) == SOCKET_ERROR)
			throw  GetErrorMsgText(WSAGetLastError());

		/*if (setsockopt(GS, SOL_SOCKET, SO_RCVTIMEO, (char*)&port, sizeof(int)) == SOCKET_ERROR)
			throw  GetErrorMsgText(WSAGetLastError());

		if (ioctlsocket(GS, FIONBIO, &nonblk) == SOCKET_ERROR)
			throw SetErrorMsgText("ioctlsocket:", WSAGetLastError());*/

		while (notErr)
		{
			if (recvfrom(GS, buf, 50, NULL, (LPSOCKADDR)&serv, &lfrom) == SOCKET_ERROR)
				throw WSAGetLastError();

			if (strcmp(buf, call) == 0)
			{
				numOfServers++;
				return true;
			}
			else
				return false;
		}
	}
	catch (int err_code)
	{
		if (err_code != WSAETIMEDOUT)
		{
			throw GetErrorMsgText(WSAGetLastError());
		}
		else if (numOfServers == 0) {
			return false;
		}
		else {
			return true;
		}
	}
}

int main(int argc, char* argv[])
{
	setlocale(LC_ALL, "");
	WSADATA wsaData;


	clock_t start, stop;
	try
	{
		if (WSAStartup(MAKEWORD(2, 0), &wsaData) != 0)
			throw SetErrorMsgText("Startup: ", WSAGetLastError());
		cout << "---------Connection--------" << endl;
		bool resServ = GetServer(name, 2000, (LPSOCKADDR)&serv, sizeof(serv));
		if (resServ)
		{
			cout << "Amount servers: " << numOfServers << endl;
			cout << "Connection successful" << endl;
			cout << "Server: " << "\nIP: " <<inet_ntoa(serv.sin_addr) << "\nPort:" << htons(serv.sin_port) << endl;
		}
		else
		{
			cout << "Failed connection" << endl;
		}

		if ((cC = socket(AF_INET, SOCK_STREAM, NULL)) == INVALID_SOCKET) //формат адреса ,тип сокета,протокол
			throw SetErrorMsgText("socket: ", WSAGetLastError());

		if ((connect(cC, (sockaddr*)&serv, sizeof(serv))) == SOCKET_ERROR)
			throw SetErrorMsgText("connect: ", WSAGetLastError());

		int cmd;
		string obufi;
		string buff;
		
		do {
			cout << "Commands: " << endl;
			cout << "1 - Echo" << endl;
			cout << "2 - Time" << endl;
			cout << "3 - Rand" << endl;
			cout << "4 - Exit" << endl;
			cout << "Choose command: " << endl;
		
		cin >> cmd;
		start = clock();
		switch (cmd)
		{
		case 1:
			obufi = "Echo";
			if ((lobuf = send(cC, obufi.c_str(), strlen(obufi.c_str()) + 1, NULL)) == SOCKET_ERROR)
				throw SetErrorMsgText("send: ", WSAGetLastError());
			cout << "Enter message to server: ";
			cin >> buff;
			if ((lobuf = send(cC, buff.c_str(), strlen(buff.c_str()) + 1, NULL)) == SOCKET_ERROR)
				throw SetErrorMsgText("send: ", WSAGetLastError());
			if ((libuf = recv(cC, ibuf, sizeof(ibuf), NULL)) == SOCKET_ERROR)
				throw SetErrorMsgText("recv: ", WSAGetLastError());
			cout << "Server response: " << ibuf << endl;
			break;

		case 2:
			obufi = "Time";
			if ((lobuf = send(cC, obufi.c_str(), strlen(obufi.c_str()) + 1, NULL)) == SOCKET_ERROR)
				throw SetErrorMsgText("send: ", WSAGetLastError());
			if ((lobuf = send(cC, obufi.c_str(), strlen(obufi.c_str()) + 1, NULL)) == SOCKET_ERROR)
				throw SetErrorMsgText("send: ", WSAGetLastError());
			if ((libuf = recv(cC, ibuf, sizeof(ibuf), NULL)) == SOCKET_ERROR)
				throw SetErrorMsgText("recv: ", WSAGetLastError());
			cout << "Server response: " << ibuf << endl;
			break;

		case 3:
			obufi = "Rand";
			if ((lobuf = send(cC, obufi.c_str(), strlen(obufi.c_str()) + 1, NULL)) == SOCKET_ERROR)
				throw SetErrorMsgText("send: ", WSAGetLastError());
			if ((lobuf = send(cC, obufi.c_str(), strlen(obufi.c_str()) + 1, NULL)) == SOCKET_ERROR)
				throw SetErrorMsgText("send: ", WSAGetLastError());
			if ((libuf = recv(cC, ibuf, sizeof(ibuf), NULL)) == SOCKET_ERROR)
				throw SetErrorMsgText("recv: ", WSAGetLastError());
			cout << "Server response: " << ibuf << endl;
			//Sleep(500);
			break;
		case 4:
			obufi = "Exit";
			exit(0);
			break;
		default:
			cout << "Invalid input!" << endl;
			break;
		}
		
		stop = clock();
		cout << "Exchange time: " << ((stop - start) / CLOCKS_PER_SEC) << endl;
		} while (cmd != -1);
		if (closesocket(cC) == SOCKET_ERROR)
			throw SetErrorMsgText("closesocket: ", WSAGetLastError());
		if (WSACleanup() == SOCKET_ERROR)
			throw SetErrorMsgText("Cleanup: ", WSAGetLastError());
	}
	catch (string errorMsgText)
	{
		cout << endl << errorMsgText << endl;
	}
	system("pause");
	return 0;
}