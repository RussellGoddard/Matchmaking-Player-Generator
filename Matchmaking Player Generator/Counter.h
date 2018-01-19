#pragma once

//forward declare dependancies

//include dependancies
#include <chrono>
#include <iostream>
#include <thread> 


//variables
typedef int*(*SyncCounter)(void);
SyncCounter syncCounter;
int countWait[2];

//class
class Counter {
public:

	int GetCount();
	int GetWaitTime();
	void SetWaitTime(int);
	int GetUpdateTime();
	void SetUpdateTime(int);

	void StartCount();
	void ResetCount();
	int* SyncCount();

	Counter(int set = 1000, int update = 10);
	~Counter();
private:
	void IncCounter();

	int count;
	int waitTime;
	int updateTime;
	std::thread counterThread;
};

//this has to go after the counter declaration
Counter *cppCount;


int Counter::GetCount() { return count; }

int Counter::GetWaitTime() { return waitTime; }

void Counter::SetWaitTime(int set) { waitTime = set; }

int Counter::GetUpdateTime() { return updateTime; }

void Counter::SetUpdateTime(int update) { updateTime = update; }

void Counter::StartCount() { counterThread = std::thread([this] { this->IncCounter(); }); }

void Counter::ResetCount() { count = 0; }

Counter::Counter(int wait /*= 1000*/, int update /*= 10*/) {
	count = 0;

	updateTime = update;
	waitTime = wait;
}

Counter::~Counter() {
	counterThread.~thread();

	count = NULL;
	waitTime = NULL;
}

void Counter::IncCounter() {
	while (true) {
		std::this_thread::sleep_for(std::chrono::milliseconds(waitTime));

		++count;

		if (count % updateTime == 0) {
			syncCounter();
		}
	}
}

int* Counter::SyncCount() {
	countWait[0] = count;
	countWait[1] = waitTime;

	return countWait;
}

extern "C" __declspec(dllexport) void CreateCounter(int set, int update) {
	cppCount = new Counter(set, update);
	cppCount->StartCount();
}

extern "C" __declspec(dllexport) int GetCount() {
	return cppCount->GetCount();
}

extern "C" __declspec(dllexport) int* cpp_SyncCounter() {
	return cppCount->SyncCount();
}