// stdafx.h: включаемый файл для стандартных системных включаемых файлов
// или включаемых файлов для конкретного проекта, которые часто используются, но
// нечасто изменяются
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

#define WIN32_LEAN_AND_MEAN             // Исключите редко используемые компоненты из заголовков Windows
// Файлы заголовков Windows
#include <windows.h>

enum TalkersCmd { Start, Stop, Exit, Statistics, Wait, Shutdown, Getcommand };

struct Param
{
	TalkersCmd cmd;
	int port;
};

struct Contact         // элемент списка подключений       
{
	enum TE {               // состояние  сервера подключения  
		EMPTY,              // пустой элемент списка подключений 
		ACCEPT,             // подключен (accept), но не обслуживается
		CONTACT             // передан обслуживающему серверу  
	}    type;     // тип элемента списка подключений 
	enum ST {               // состояние обслуживающего сервера  
		WORK,               // идет обмен данными с клиентом
		ABORT,              // обслуживающий сервер завершился не нормально 
		TIMEOUT,            // обслуживающий сервер завершился по времени 
		FINISH              // обслуживающий сервер завершился  нормально 
	}      sthread; // состояние  обслуживающего сервера (потока)

	SOCKET      s;         // сокет для обмена данными с клиентом
	SOCKADDR_IN prms;      // параметры  сокета 
	int         lprms;     // длина prms 
	HANDLE      hthread;   // handle потока (или процесса) 
	HANDLE      htimer;    // handle таймера
	HANDLE		serverHThtead;// handle обслуживающего сервера который в последствие может зависнуть
	bool        TimerOff = false;  // Метка срабатывания таймера

	char msg[50];           // сообщение 
	char srvname[15];       //  наименование обслуживающего сервера 

	Contact(TE t = EMPTY, const char* namesrv = "") // конструктор 
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
// список подключений  

ListContact Contacts;

// Асинхронная функция срабатывания таймера

// установите здесь ссылки на дополнительные заголовки, требующиеся для программы
