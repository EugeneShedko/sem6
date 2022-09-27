// stdafx.h: ���������� ���� ��� ����������� ��������� ���������� ������
// ��� ���������� ������ ��� ����������� �������, ������� ����� ������������, ��
// ������� ����������
//

#pragma once
#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include "Winsock2.h"              
#pragma comment(lib,"WS2_32.lib")
#include <string>
#include <list>
#include <time.h>

using namespace std;

#define WIN32_LEAN_AND_MEAN             // ��������� ����� ������������ ���������� �� ���������� Windows
// ����� ���������� Windows
#include <windows.h>

enum TalkersCmd { Start, Stop, Exit, Statistics, Wait, Shutdown, Getcommand };

struct Param
{
	TalkersCmd cmd;
	int port;
};

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
// ������ �����������  

ListContact Contacts;

// ����������� ������� ������������ �������

// ���������� ����� ������ �� �������������� ���������, ����������� ��� ���������
