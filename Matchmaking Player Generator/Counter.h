#pragma once

//forward declare dependancies

//include dependancies
#include <chrono>
#include <iostream>
#include <thread> 


//variables

//class
class Counter {
public:

	int GetCount();
	int GetWaitTime();
	void SetWaitTime(int);

	void StartCount();
	void ResetCount();

	Counter(int set = 1000);
	~Counter();
private:
	void IncCounter();

	int count;
	int waitTime;
	std::thread counterThread;
};

int Counter::GetCount() { return count; }

int Counter::GetWaitTime() { return waitTime; }

void Counter::SetWaitTime(int set) { waitTime = set; }

void Counter::StartCount() { counterThread = std::thread([this] { this->IncCounter(); }); }

void Counter::ResetCount() { count = 0; }

Counter::Counter(int set /*= 1000*/) {
	count = 0;
	waitTime = set;
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
	}
}

void counter() {

	int count = 0;

	while (count < 10) {
		std::cout << count << std::endl;

		std::this_thread::sleep_for(std::chrono::seconds(1));
		++count;
	}

}


//extern "C" __declspec(dllexport) int* SyncCount() {
//	
//}
