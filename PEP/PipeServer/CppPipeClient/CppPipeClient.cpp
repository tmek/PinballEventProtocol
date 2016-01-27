// CppPipeClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <windows.h> 
#include <stdio.h>
#include <conio.h>
#include <tchar.h>

#include "PipeClient.h"

#define PIPE_BUFFER_SIZE 8192
char buffer[PIPE_BUFFER_SIZE];

int _tmain(int argc, _TCHAR* argv[])
{
	LPTSTR pipeName = TEXT("\\\\.\\pipe\\PinballEvents");

	PipeClient* client = new PipeClient(pipeName);

	client->Connect();
	
	while (true) {
		if (client->HasDataAvailable())
		{
			client->Read(buffer, PIPE_BUFFER_SIZE);
			printf("Read %d bytes.\n", client->bytesReadCount);
		}

		printf("Sleeping...\n");
		Sleep(100);
	}

	client->Disconnect();

	return 0;
}

