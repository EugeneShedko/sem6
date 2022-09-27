#pragma once
#include "includes.h"

struct ServerParams
{
	u_short port;
	u_short responsePort;
	string pipeName;
	string serviceName;
	HANDLE procedureHandlerThread;
};