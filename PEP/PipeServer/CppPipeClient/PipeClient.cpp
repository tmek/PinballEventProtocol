#include "stdafx.h"
#include "PipeClient.h"


PipeClient::PipeClient(LPCWSTR pipeName)
{
	this->pipeName = pipeName;
}


PipeClient::~PipeClient()
{
}


BOOL PipeClient::Connect()
{
	hPipe = CreateFile(
		pipeName,		// pipe name 
		GENERIC_READ |  // read and write access 
		GENERIC_WRITE,
		0,              // no sharing 
		NULL,           // default security attributes
		OPEN_EXISTING,  // opens existing pipe 
		0,              // default attributes 
		NULL);          // no template file 

	if (hPipe != INVALID_HANDLE_VALUE)
		return true;

	_tprintf(TEXT("Could not open pipe. GLE=%d\n"), GetLastError());
	return false;
}


BOOL PipeClient::Write(char* buffer, int size)
{
	return WriteFile(
		hPipe,                  // pipe handle 
		buffer,					// message 
		size,					// message length 
		&bytesWrittenCount,     // bytes written 
		NULL);                  // not overlapped 
}


BOOL PipeClient::HasDataAvailable()
{
	DWORD bytesAvailable;
	PeekNamedPipe(hPipe, NULL, 0, 0, &bytesAvailable, NULL);
	return (bytesAvailable > 0);
}


BOOL PipeClient::Read(char* buffer, int size)
{
	return ReadFile(
		hPipe,		// pipe handle 
		buffer,		// buffer to receive reply 
		size, // size of buffer 
		&bytesReadCount, // number of bytes read 
		NULL);		// not overlapped 
}

void PipeClient::Disconnect()
{
	CloseHandle(hPipe);
}

