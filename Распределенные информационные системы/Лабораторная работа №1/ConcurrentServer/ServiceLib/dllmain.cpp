// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include "DefineTableService.h"
#include "Services.h"

BEGIN_TABLESERVICE
    { "ECHO", services::EchoServer },
    { "TIME", services::TimeServer },
    { "RAND", services::RandServer }
END_TABLESERVICE;

extern "C" __declspec(dllexport) HANDLE  ServeClient(const char* id, LPVOID prm)
{
    HANDLE rc = NULL;
    int  i = 0;
    while (i < SIZETS && strcmp(TABLESERVICE_ID(i), id) != 0)i++;
    if (i < SIZETS)
        rc = CreateThread(NULL, NULL,
            TABLESERVICE_FN(i), prm, NULL, NULL);
    return rc;
};

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

