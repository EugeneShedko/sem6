#include "pch.h"
#include "Services.h"
#include <random>

namespace services
{
	DWORD _stdcall RandServer(LPVOID prm)
	{
		try
		{
			srand(time(NULL));
			auto contact = static_cast<Contact*>(prm);
			auto socket = contact->s;

			try
			{
				QueueUserAPC(reinterpret_cast<PAPCFUNC>(services::StartServingAsync), contact->procedureHandlerThread, reinterpret_cast<ULONG_PTR>(contact));

				string msg;
				while (true)
				{
					char buffer[1024];
					int result = recv(socket, buffer, sizeof(buffer), NULL);

					if (result == SOCKET_ERROR) {
						// ошибка получения данных
						std::cerr << "recv failed: " << result << "\n";
						contact->sthread = Contact::ST::ABORT;
						break;
					}
					else if (result == 0) {
						// соединение закрыто клиентом
						std::cerr << "connection closed...\n";
						contact->sthread = Contact::ST::ABORT;
						break;
					}
					else {
						buffer[result] = '\0';
						if (string(buffer) == "") break;

						_itoa_s(rand() % 9999, buffer, 10);

						if (send(contact->s, buffer, sizeof(buffer), 0) < 0)
						{
							contact->sthread = Contact::ST::ABORT;
							break;
						}
					}
				}

			}
			catch (string str)
			{
				cout << str << endl;
			}

			contact->sthread = Contact::ST::FINISH;
			closesocket(contact->s);

			QueueUserAPC(reinterpret_cast<PAPCFUNC>(FinishServingAsync), contact->procedureHandlerThread, reinterpret_cast<ULONG_PTR>(contact));
		}
		catch (...)
		{
			cout << "EchoServer: Fatal error...";
		}

		return 0;
	}
}
