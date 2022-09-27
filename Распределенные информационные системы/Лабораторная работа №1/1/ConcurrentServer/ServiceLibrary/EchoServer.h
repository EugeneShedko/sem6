#pragma once

#include "pch.h"
#include "Error.h"

DWORD WINAPI EchoServer(LPVOID lParam)
{
	// ��� ��������
	DWORD rc = 0;
	Contact* client = (Contact*)lParam;
	//// ������ � ������� ����������� ��������� ��������� � ������ ������������
	QueueUserAPC(ASStartMessage, client->hthread, (DWORD)client);
	try
	{
		client->sthread = Contact::WORK;
		char ibuf[50], obuf[50];
		cout << client->TimerOff;
		while (client->TimerOff == false)
		{
			if (recv(client->s, ibuf, sizeof(ibuf), NULL) == SOCKET_ERROR)
				throw SetErrorMsgText("recv: ", WSAGetLastError());
			if (strcmp(ibuf, "") == 0)
				break;

			string obuf = ibuf;

			if (strcmp(ibuf, "exit") != 0)
			{
				if (client->TimerOff != false)
				{
					client->sthread = Contact::FINISH;
					break;
				}
				if ((send(client->s, ibuf, sizeof(ibuf), NULL)) == SOCKET_ERROR)
					throw  SetErrorMsgText("Send:", WSAGetLastError());
			}
			else
			{
				client->sthread = Contact::FINISH;
				break;
			}
		}
		if (client->TimerOff == false)
		{
			// ��������� ������
			CancelWaitableTimer(client->htimer);
			if ((send(client->s, obuf, sizeof(obuf) + 1, NULL)) == SOCKET_ERROR)
				throw  SetErrorMsgText("Send:", WSAGetLastError());
			// ������ ����� �������� ����������
			client->sthread = Contact::FINISH;
			// ������ � ������� ����������� ��������� ��������� � ���������� ������������
			QueueUserAPC(ASFinishMessage, client->hthread, (DWORD)client);
		}
	}
	catch (string errorMsgText)
	{
		std::cout << errorMsgText << std::endl;
		CancelWaitableTimer(client->htimer);
		client->sthread = Contact::ABORT;
	}
	// ��������� �����
	ExitThread(rc);
}