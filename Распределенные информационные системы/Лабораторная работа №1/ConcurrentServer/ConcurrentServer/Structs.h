#pragma once
#include <WinSock2.h>
#include <WS2tcpip.h>
#pragma comment(lib, "WS2_32.lib")
#include <iostream>
#include <list>
#include <string.h>
#include "CMDCommand.h"
#include "AnotherFunc.h"
#include "Structs.h"

struct ServerParams
{
	u_short port;
	u_short responsePort;
	string pipeName;
	string serviceName;
	string dllName;
	HANDLE procedureHandlerThread;
	HMODULE hLib;
};

struct Contact // ������� ������ �����������
{
	enum TE { // ��������� ������� �����������
		EMPTY, // ������ ������� ������ �����������
		ACCEPT, // ��������� (accept), �� �� �������������
		CONTACT // ������� �������������� �������
	} type; // ��� �������� ������ �����������

	enum ST { // ��������� �������������� �������
		WORK, // ���� ����� ������� � ��������
		ABORT, // ������������� ������ ���������� �� ���������
		TIMEOUT, // ������������� ������ ���������� �� �������
		FINISH // ������������� ������ ���������� ���������
	} sthread; // ��������� �������������� ������� (������)

	SOCKET s; // ����� ��� ������ ������� � ��������
	SOCKADDR_IN prms; // ��������� ������
	int lprms; // ����� prms
	HANDLE hthread; // handle ������ (��� ��������)
	HANDLE htimer; // handle �������
	char msg[50]; // ���������
	char srvname[15]; // ������������ �������������� �������
	HANDLE procedureHandlerThread;

	Contact(TE t = EMPTY, const char* namesrv = "") // �����������
	{
		memset(&prms, 0, sizeof(SOCKADDR_IN));
		lprms = sizeof(SOCKADDR_IN);
		type = t;
		strcpy(srvname, namesrv);
		msg[0] = 0;
	};

	void SetST(ST sth, const char* m = "")
	{
		sthread = sth;
		strcpy(msg, m);
	}
};

struct ServerStats
{
	DWORD connectionsOverTime;
	DWORD currentConnections;
	DWORD denialsOverTime;
};
