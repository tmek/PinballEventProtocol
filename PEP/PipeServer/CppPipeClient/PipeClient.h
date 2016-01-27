#pragma once

#include "windows.h"

class PipeClient
{
	LPCWSTR pipeName;
	HANDLE hPipe;

public:
	DWORD bytesReadCount = 0;		// todo: need to be readonly
	DWORD bytesWrittenCount = 0;	// todo: need to be readonly

	PipeClient(LPCWSTR pipeName);
	~PipeClient();
	BOOL Connect();
	BOOL HasDataAvailable();
	BOOL Write(char* buffer, int size);
	BOOL Read(char* buffer, int size);
	void Disconnect();
};

