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

struct Contact // элемент списка подключений
{
	enum TE { // состояние сервера подключения
		EMPTY, // пустой элемент списка подключений
		ACCEPT, // подключен (accept), но не обслуживается
		CONTACT // передан обслуживающему серверу
	} type; // тип элемента списка подключений

	enum ST { // состояние обслуживающего сервера
		WORK, // идет обмен данными с клиентом
		ABORT, // обслуживающий сервер завершился не нормально
		TIMEOUT, // обслуживающий сервер завершился по времени
		FINISH // обслуживающий сервер завершился нормально
	} sthread; // состояние обслуживающего сервера (потока)

	SOCKET s; // сокет для обмена данными с клиентом
	SOCKADDR_IN prms; // параметры сокета
	int lprms; // длина prms
	HANDLE hthread; // handle потока (или процесса)
	HANDLE htimer; // handle таймера
	char msg[50]; // сообщение
	char srvname[15]; // наименование обслуживающего сервера
	HANDLE procedureHandlerThread;

	Contact(TE t = EMPTY, const char* namesrv = "") // конструктор
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
