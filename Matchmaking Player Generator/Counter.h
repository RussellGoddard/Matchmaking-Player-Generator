#pragma once

//forward declare dependancies

//include dependancies
#include <chrono>
#include <iostream>
#include <thread> 


//variables

//class


void counter() {

	int count = 0;

	while (count < 10) {
		std::cout << count << std::endl;

		std::this_thread::sleep_for(std::chrono::seconds(1));
		++count;
	}

}