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
	std::string get_current_time();

	void CALLBACK StartServingAsync(Contact* contact);
	void CALLBACK FinishServingAsync(Contact* contact);

	DWORD _stdcall EchoServer(LPVOID prm);

	DWORD _stdcall TimeServer(LPVOID prm);

	DWORD _stdcall RandServer(LPVOID prm);
}