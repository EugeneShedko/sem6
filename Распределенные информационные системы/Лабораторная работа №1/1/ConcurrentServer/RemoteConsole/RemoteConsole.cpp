#define _CRT_SECURE_NO_WARNINGS
#include "Windows.h"
#include <iostream>
#include <string>
#include "ErrorHandling.h"

using namespace std;


void ServerResponse(HANDLE hPipe, char w[20]);
int main()
{
	setlocale(LC_ALL, "rus");
	HANDLE hPipe;
	int cmd;
	char wbuf[20];
	DWORD cbWritten;
	DWORD cbRead;
	char rbuf[512];

	try
	{
		if ((hPipe = CreateFile(
			L"\\\\HOME-PC\\pipe\\jeka",
			GENERIC_READ | GENERIC_WRITE,
			FILE_SHARE_READ | FILE_SHARE_WRITE,
			NULL, OPEN_EXISTING, NULL,
			NULL)) == INVALID_HANDLE_VALUE)
			throw  SetPipeError("createfile: ", GetLastError());

		while (true)
		{
			cout << "-----Commands-----" << endl;
			cout << "1 - Start" << endl;
			cout << "2 - Stop" << endl;
			cout << "3 - Exit" << endl;
			cout << "4 - Statistics" << endl;
			cout << "5 - Wait" << endl;
			cout << "6 - Shutdowm" << endl;
			cout << "7 - GetCommand" << endl;
			cout <<endl<< "------------------" << endl;
			cout <<endl<< "Choose command:" << endl;
			cin >> cmd;
			switch (cmd) 
			{
			case 1:
				strcpy_s(wbuf, "start");
				system("cls");
				break;
			case 2:
				strcpy_s(wbuf, "stop");
				system("cls");
				break;
			case 3:
				strcpy_s(wbuf, "exit");
				system("cls");
				break;
			case 4:
				strcpy_s(wbuf, "statistics");
				system("cls");
				break;
			case 5:
				strcpy_s(wbuf, "wait");
				system("cls");
				break;
			case 6:
				strcpy_s(wbuf, "shutdown");
				system("cls");
				break;
			case 7:
				strcpy_s(wbuf, "getcommand");
				system("cls");
				break;
			default:
				cout << "Invalid command! Please try again." << endl;
				break;
			}

			if (!WriteFile(hPipe, wbuf, sizeof(wbuf), &cbWritten, NULL))
				throw  SetPipeError("writefile:", GetLastError());

			if (ReadFile(hPipe, rbuf, sizeof(rbuf), &cbRead, NULL))
				cout << "receive:  " << rbuf << endl;
			/*switch (cmd) 
			{
				case 1:
					wbuf[20] = NULL;
					sprintf(wbuf, "start");
					ServerResponse(hPipe, wbuf);
					break;
				case 2:
					char w[20];
					sprintf(wbuf, "stop");
					ServerResponse(hPipe, w);
					break;
				case 3:
					char wbuf[20];
					sprintf(wbuf, "exit");
					ServerResponse(hPipe, wbuf);
					break;
				case 4:
					char wbuf[20];
					sprintf(wbuf, "statistics");
					ServerResponse(hPipe, wbuf);
					break;
				case 5:
					char wbuf[20];
					sprintf(wbuf, "wait");
					ServerResponse(hPipe, wbuf);
					break;
				case 6:
					char wbuf[20];
					sprintf(wbuf, "shutdown");
					ServerResponse(hPipe, wbuf);
					break;
				case 7:
					char wbuf[20];
					sprintf(wbuf, "getcommand");
					ServerResponse(hPipe, wbuf);
					break;
				
			}*/
		}
		

	}
	catch (string ErrorPipeText)
	{
		cout << endl << ErrorPipeText;
	}
	CloseHandle(hPipe);
	system("pause");

}

void ServerResponse(HANDLE hPipe, char wbuf[20]) 
{
	DWORD cbWritten;
	DWORD cbRead;
	char rbuf[512];
	if (!WriteFile(hPipe, wbuf, strlen(wbuf) + 1, &cbWritten, NULL))
		throw SetPipeError("WriteFile: ", GetLastError());

	if (!ReadFile(hPipe, rbuf, sizeof(rbuf) + 1, &cbRead, NULL))
		throw SetPipeError("ReadFile: ", GetLastError());

	cout << rbuf << endl;
	return;
}