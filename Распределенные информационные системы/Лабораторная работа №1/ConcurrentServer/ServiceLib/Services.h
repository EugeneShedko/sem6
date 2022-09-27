#pragma once
#include <iostream>
#include <Windows.h>
#include <chrono>
#include <WinSock2.h>
#include <WS2tcpip.h>
#pragma comment(lib, "WS2_32.lib")
#include <iostream>
#include <list>
#include <string.h>

extern "C" __declspec(dllexport) HANDLE SSS(char* id, LPVOID prm);

namespace services
{
	using namespace std;
	using namespace chrono;

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
	std::string get_current_time();

	void CALLBACK StartServingAsync(Contact* contact);
	void CALLBACK FinishServingAsync(Contact* contact);

	DWORD _stdcall EchoServer(LPVOID prm);

	DWORD _stdcall TimeServer(LPVOID prm);

	DWORD _stdcall RandServer(LPVOID prm);
}