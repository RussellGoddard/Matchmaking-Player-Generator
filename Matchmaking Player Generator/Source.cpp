#include <iomanip>
#include <iostream>
#include <memory>

#include "Counter.h"
#include "DataForUI.h"
#include "GetRandom.h"
#include "Player.h"

std::vector<std::pair<std::string, int>> numberOfPlayersDivision;


int run() {
	//simply a test program for generating players
	//shared_ptr is used because eventually this pointer will be in 3 different places (I believe), the list of players, the players queued sorted by when they queued, and the players in queue sorted by mmr

	std::vector<std::shared_ptr<Player>> playerVec;
	int sum = 0;
	const int PLAYER_BASE = 1000000; //player base size, change this value to change how many players are created

	std::pair<std::string, int> bronze = std::make_pair("Bronze", 0);
	std::pair<std::string, int> silver = std::make_pair("Silver", 0);
	std::pair<std::string, int> gold = std::make_pair("Gold", 0);
	std::pair<std::string, int> plat = std::make_pair("Plat", 0);
	std::pair<std::string, int> diamond = std::make_pair("Diamond", 0);
	std::pair<std::string, int> masters = std::make_pair("Masters", 0);
	std::pair<std::string, int> challenger = std::make_pair("Challenger", 0);

	numberOfPlayersDivision.push_back(bronze);
	numberOfPlayersDivision.push_back(silver);
	numberOfPlayersDivision.push_back(gold);
	numberOfPlayersDivision.push_back(plat);
	numberOfPlayersDivision.push_back(diamond);
	numberOfPlayersDivision.push_back(masters);
	numberOfPlayersDivision.push_back(challenger);


	//generate 1000 players, output the % of each in each division
	for (int i = 0; i < PLAYER_BASE; ++i) {
		std::shared_ptr<Player> newPlayer = std::make_shared<Player>();
		playerVec.push_back(newPlayer);
	}

	//count each person in each bracket
	for (auto obj : playerVec) {
		//if else if statements because we only care about bronze/silver/gold/plat etc not each division inside
		if (obj->GetMmr() < 600) { //bronze
			++numberOfPlayersDivision.at(0).second;
		}
		else if (obj->GetMmr() < 1100) { //silver
			++numberOfPlayersDivision.at(1).second;
		}
		else if (obj->GetMmr() < 1500) { //gold
			++numberOfPlayersDivision.at(2).second;
		}
		else if (obj->GetMmr() < 2000) { //plat
			++numberOfPlayersDivision.at(3).second;
		}
		else if (obj->GetMmr() < 2500) { //diamond
			++numberOfPlayersDivision.at(4).second;
		}
		else if (obj->GetMmr() < 2800) { //masters
			++numberOfPlayersDivision.at(5).second;
		}
		else if (obj->GetMmr() < 3000) { //challenger
			++numberOfPlayersDivision.at(6).second;
		}
	}


	for (auto i : numberOfPlayersDivision) {
		std::cout << std::setw(13) << std::setfill(' ') << std::left << i.first + ": " << 
			std::right << std::to_string(i.second) << ' ' + std::to_string((i.second / static_cast<double>(PLAYER_BASE)) * 100) << std::endl;
		sum += i.second;
	}

	std::cout << std::endl << "Sum of Players: " << std::to_string(sum) << std::endl;


	/*std::cout << std::endl;

	std::thread threadCounter(counter);

	threadCounter.join();*/

	//system("pause");
	return 0;
}

extern "C" __declspec(dllexport) int* PlayerDistro() {

	int* test = new int[8];
	int sum = 0;

	run();

	for (int i = 0; i < 7; ++i) {
		test[i] = numberOfPlayersDivision.at(i).second;
		sum += numberOfPlayersDivision.at(i).second;
	}

	test[7] = sum;

	return test;
}

extern "C" __declspec(dllexport) void DeleteArray(int* pArray) {
	delete[] pArray;
}