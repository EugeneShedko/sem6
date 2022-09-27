#include <iostream>
#include <string>
#include <Windows.h>

std::string GetErrorMsgText(int code)
{
	std::string msgText;
	switch (code)
	{
	case WSAEINTR: msgText = "������ ������� ��������"; break;
	case WSAEACCES: msgText = "���������� ����������"; break;
	case WSAEFAULT: msgText = "���������� ����������"; break;
	case WSAEINVAL: msgText = "������ � ���������"; break;
	case WSAEMFILE: msgText = "������� ����� ������ �������"; break;
	case WSAEWOULDBLOCK: msgText = "������ �������� ����������"; break;
	case WSAEINPROGRESS: msgText = "�������� � �������� ��������"; break;
	case WSAEALREADY: msgText = "�������� ��� �����������"; break;
	case WSAENOTSOCK: msgText = "����� ����� �����������"; break;
	case WSAEDESTADDRREQ: msgText = "��������� ����� ������������"; break;
	case WSAEMSGSIZE: msgText = "��������� ������� �������"; break;
	case WSAEPROTOTYPE: msgText = "������������ ��� ��������� ��� ������"; break;
	case WSAENOPROTOOPT: msgText = "������ � ����� ���������"; break;
	case WSAEPROTONOSUPPORT: msgText = "�������� �� ��������������"; break;
	case WSAESOCKTNOSUPPORT: msgText = "��� ������ �� ��������������"; break;
	case WSAEOPNOTSUPP: msgText = "�������� �� ��������������"; break;
	case WSAEPFNOSUPPORT: msgText = "��� ���������� �� ��������������"; break;
	case WSAEAFNOSUPPORT: msgText = "��� ������� �� �������������� ����������"; break;
	case WSAEADDRINUSE: msgText = "����� ��� ������������"; break;
	case WSAEADDRNOTAVAIL: msgText = "����������� ����� �� ����� ���� �����������"; break;
	case WSAENETDOWN: msgText = "���� ���������"; break;
	case WSAENETUNREACH: msgText = "���� �� ���������"; break;
	case WSAENETRESET: msgText = "���� ��������� ����������"; break;
	case WSAECONNABORTED: msgText = "����������� ����� �����"; break;
	case WSAECONNRESET: msgText = "����� �������������"; break;
	case WSAENOBUFS: msgText = "�� ������� ������ ��� �������"; break;
	case WSAEISCONN: msgText = "����� ��� ���������"; break;
	case WSAENOTCONN: msgText = "����� �� ���������"; break;
	case WSAESHUTDOWN: msgText = "������ ��������� send : ����� �������� ������"; break;
	case WSAETIMEDOUT: msgText = "���������� ���������� ��������  �������"; break;
	case WSAECONNREFUSED: msgText = "���������� ���������"; break;
	case WSAEHOSTDOWN: msgText = "���� � ����������������� ���������"; break;
	case WSAEHOSTUNREACH: msgText = "��� �������� ��� �����"; break;
	case WSAEPROCLIM: msgText = "������� ����� ���������"; break;
	case WSASYSNOTREADY: msgText = "���� �� �������� "; break;
	case WSAVERNOTSUPPORTED: msgText = "������ ������ ����������"; break;
	case WSANOTINITIALISED: msgText = "�� ��������� ������������� WS2_32.DLL"; break;
	case WSAEDISCON: msgText = "����������� ����������"; break;
	case WSATYPE_NOT_FOUND: msgText = "����� �� ������"; break;
	case WSAHOST_NOT_FOUND: msgText = "���� �� ������"; break;
	case WSATRY_AGAIN: msgText = "������������������ ���� �� ������"; break;
	case WSANO_RECOVERY: msgText = "�������������� ������"; break;
	case WSANO_DATA: msgText = "��� ������ ������������ ����"; break;
		/*case WSA_INVALID_HANDLE: msgText = "��������� ���������� ������� � �������"; break;
		case WSA_INVALID_PARAMETER: msgText = "���� ��� ����� ���������� � �������"; break;
		case WSA_IO_INCOMPLETE: msgText = "������ ����� - ������ �� � ���������� ���������"; break;
		case WSA_IO_PENDING: msgText = "�������� ���������� �����"; break;
		case WSA_NOT_ENOUGH_MEMORY: msgText = "�� ���������� ������"; break;
		case WSA_OPERATION_ABORTED: msgText = "�������� ����������"; break;
		*/case WSASYSCALLFAILURE: msgText = "��������� ���������� ���������� ������"; break;
		default: msgText = "***ERROR***"; break;
			break;
	}
	return msgText;
}

std::string SetPipeError(std::string msgText, int code)
{
	return msgText + GetErrorMsgText(code);
}


void main()
{
	HANDLE _handle;
	char buf[128];
	std::string mess;
	DWORD lBuf, bWrite = 128;
	int numMess;

	try
	{
		if ((_handle = CreateFile(TEXT("\\\\HOME-PC\\pipe\\NordStream2"), GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, 0, NULL)) == INVALID_HANDLE_VALUE)
			throw SetPipeError("CreateFile: ", GetLastError());
	
		while (true)
		{
			std::cout << "Print operation: ";
			std::cin >> mess;

			if (!WriteFile(_handle, mess.c_str(), sizeof(mess), &lBuf, NULL))
				throw SetPipeError("WriteFile: ", GetLastError());

			if (mess == "EXIT") break;

			if (!ReadFile(_handle, buf, sizeof(buf), &lBuf, NULL))
				throw SetPipeError("ReadFile: ", GetLastError());

			std::cout << "Server send: " << buf << std::endl;
		}
		CloseHandle(_handle);
	}
	catch (std::string ErrorPipeText)
	{
		std::cout << ErrorPipeText << std::endl;
	}
	system("pause");
}