#pragma once
#include "includes.h"
using namespace std;

DWORD WINAPI AcceptServerThread(LPVOID command_param)
{
	while (true)
	{
		cout << "AcceptServerThread" << endl;
		Sleep(5000);
	}
	return 0;
}