#include <iomanip>
#include <iostream>
#include <memory>

#include "Counter.h"
#include "DataForUI.h"
#include "GetRandom.h"
#include "Player.h"

//variables that can't go out of scope, relevant ones are grouped together

//callbacks, used to tell c# to update a variable
typedef void(*foo)(void);
foo test;
typedef void(*UpdatePlayers)(void);
UpdatePlayers updatePlayers;


//order is always bronze 5, bronze 4, bronze 3, bronze 2, bronze 1 (repeat for silver, gold, plat, diamond) masters, challenger    total size: 27
int numberOfPlayersBracket[NUMBER_OF_BRACKETS + 8]; //+ 8 for: //bronzeTotal, silverTotal, goldTotal, platTotal, diamondTotal, masters(repeat), chall(repeat), total
std::vector<std::shared_ptr<Player>> playerVec;


//function at the start that is called by C# and passes all the callback functions
extern "C" __declspec(dllexport) void AssignCallbacks(UpdatePlayers updatePlayersCall,  SyncCounter syncCounterCall) {
	updatePlayers = updatePlayersCall;
	syncCounter = syncCounterCall;

	return;
}

extern "C" __declspec(dllexport) void MakePlayer(int addPlayer) {
 
	//generate addPlayer players, output the % of each in each division
	for (int i = 0; i < addPlayer; ++i) {
		std::shared_ptr<Player> newPlayer = std::make_shared<Player>();
		playerVec.push_back(newPlayer);

		//add newPlayer to numberOfPlayersBracket
		if (newPlayer->GetMmr() <= BRONZE5.second) { //bronze5
			++numberOfPlayersBracket[0];
			++numberOfPlayersBracket[27]; //bronzeTotal
		}
		else if (newPlayer->GetMmr() <= BRONZE4.second) { //bronze4
			++numberOfPlayersBracket[1];
			++numberOfPlayersBracket[27]; //bronzeTotal
		}
		else if (newPlayer->GetMmr() <= BRONZE3.second) { //bronze3
			++numberOfPlayersBracket[2];
			++numberOfPlayersBracket[27]; //bronzeTotal
		}
		else if (newPlayer->GetMmr() <= BRONZE2.second) { //bronze2
			++numberOfPlayersBracket[3];
			++numberOfPlayersBracket[27]; //bronzeTotal
		}
		else if (newPlayer->GetMmr() <= BRONZE1.second) { //bronze1
			++numberOfPlayersBracket[4];
			++numberOfPlayersBracket[27]; //bronzeTotal
		}
		else if (newPlayer->GetMmr() <= SILVER5.second) { //silver5
			++numberOfPlayersBracket[5];
			++numberOfPlayersBracket[28]; //silverTotal
		}
		else if (newPlayer->GetMmr() <= SILVER4.second) { //silver4
			++numberOfPlayersBracket[6];
			++numberOfPlayersBracket[28]; //silverTotal
		}
		else if (newPlayer->GetMmr() <= SILVER3.second) { //silver3
			++numberOfPlayersBracket[7];
			++numberOfPlayersBracket[28]; //silverTotal
		}
		else if (newPlayer->GetMmr() <= SILVER2.second) { //silver2
			++numberOfPlayersBracket[8];
			++numberOfPlayersBracket[28]; //silverTotal
		}
		else if (newPlayer->GetMmr() <= SILVER1.second) { //silver1
			++numberOfPlayersBracket[9];
			++numberOfPlayersBracket[28]; //silverTotal
		}
		else if (newPlayer->GetMmr() <= GOLD5.second) { //gold5
			++numberOfPlayersBracket[10];
			++numberOfPlayersBracket[29]; //goldTotal
		}
		else if (newPlayer->GetMmr() <= GOLD4.second) { //gold4
			++numberOfPlayersBracket[11];
			++numberOfPlayersBracket[29]; //goldTotal
		}
		else if (newPlayer->GetMmr() <= GOLD3.second) { //gold3
			++numberOfPlayersBracket[12];
			++numberOfPlayersBracket[29]; //goldTotal
		}
		else if (newPlayer->GetMmr() <= GOLD2.second) { //gold2
			++numberOfPlayersBracket[13];
			++numberOfPlayersBracket[29]; //goldTotal
		}
		else if (newPlayer->GetMmr() <= GOLD1.second) { //gold1
			++numberOfPlayersBracket[14];
			++numberOfPlayersBracket[29]; //goldTotal
		}
		else if (newPlayer->GetMmr() <= PLAT5.second) { //plat5
			++numberOfPlayersBracket[15];
			++numberOfPlayersBracket[30]; //platTotal
		}
		else if (newPlayer->GetMmr() <= PLAT4.second) { //plat4
			++numberOfPlayersBracket[16];
			++numberOfPlayersBracket[30]; //platTotal
		}
		else if (newPlayer->GetMmr() <= PLAT3.second) { //plat3
			++numberOfPlayersBracket[17];
			++numberOfPlayersBracket[30]; //platTotal
		}
		else if (newPlayer->GetMmr() <= PLAT2.second) { //plat2
			++numberOfPlayersBracket[18];
			++numberOfPlayersBracket[30]; //platTotal
		}
		else if (newPlayer->GetMmr() <= PLAT1.second) { //plat1
			++numberOfPlayersBracket[19];
			++numberOfPlayersBracket[30]; //platTotal
		}
		else if (newPlayer->GetMmr() <= DIAMOND5.second) { //diamond5
			++numberOfPlayersBracket[20];
			++numberOfPlayersBracket[31]; //diamondTotal
		}
		else if (newPlayer->GetMmr() <= DIAMOND4.second) { //diamond4
			++numberOfPlayersBracket[21];
			++numberOfPlayersBracket[31]; //diamondTotal
		}
		else if (newPlayer->GetMmr() <= DIAMOND3.second) { //diamond3
			++numberOfPlayersBracket[22];
			++numberOfPlayersBracket[31]; //diamondTotal
		}
		else if (newPlayer->GetMmr() <= DIAMOND2.second) { //diamond2
			++numberOfPlayersBracket[23];
			++numberOfPlayersBracket[31]; //diamondTotal
		}
		else if (newPlayer->GetMmr() <= DIAMOND1.second) { //diamond1
			++numberOfPlayersBracket[24];
			++numberOfPlayersBracket[31]; //diamondTotal
		}
		else if (newPlayer->GetMmr() <= MASTERS.second) { //masters
			++numberOfPlayersBracket[25];
			++numberOfPlayersBracket[32]; //master (is duplicate)
		}
		else if (newPlayer->GetMmr() <= CHALLENGER.second) { //challenger
			++numberOfPlayersBracket[26];
			++numberOfPlayersBracket[33]; //master (is duplicate)
		}
		++numberOfPlayersBracket[34]; //total
	}

	updatePlayers();
	return;
}

extern "C" __declspec(dllexport) int* GetDistro() {

	//the trailing comment is the index number
	//int bronze5 = 0; //0
	//int bronze4 = 0; //1
	//int bronze3 = 0; //2
	//int bronze2 = 0; //3
	//int bronze1 = 0; //4
	//int silver5 = 0; //5
	//int silver4 = 0; //6
	//int silver3 = 0; //7
	//int silver2 = 0; //8
	//int silver1 = 0; //9
	//int gold5 = 0; //10
	//int gold4 = 0; //11
	//int gold3 = 0; //12
	//int gold2 = 0; //13
	//int gold1 = 0; //14
	//int platinum5 = 0; //15
	//int platinum4 = 0; //16
	//int platinum3 = 0; //17
	//int platinum2 = 0; //18
	//int platinum1 = 0; //19
	//int diamond5 = 0; //20
	//int diamond4 = 0; //21
	//int diamond3 = 0; //22
	//int diamond2 = 0; //23
	//int diamond1 = 0; //24
	//int masters = 0; //25
	//int challenger = 0; //26
	//int bronzeTotal = 0; //27
	//int silverTotal = 0; //28
	//int goldTotal = 0; //29
	//int platinumTotal = 0; //30
	//int diamondTotal = 0; //31
	////masters duplicate above //32
	////challenger duplicate //33
	//int total = 0; //34

	return numberOfPlayersBracket;
}

//old test function
//int run() {
//	//simply a test program for generating players
//	//shared_ptr is used because eventually this pointer will be in 3 different places (I believe), the list of players, the players queued sorted by when they queued, and the players in queue sorted by mmr
//
//	int sum = 0;
//	const int PLAYER_BASE = 1000000; //player base size, change this value to change how many players are created
//
//	int bronze = 0;
//	int silver = 0;
//	int gold = 0;
//	int plat = 0;
//	int diamond = 0;
//	int masters = 0;
//	int challenger = 0;
//
//	//generate PLAYER_BASE players, output the % of each in each division
//	for (int i = 0; i < PLAYER_BASE; ++i) {
//		std::shared_ptr<Player> newPlayer = std::make_shared<Player>();
//		playerVec.push_back(newPlayer);
//	}
//
//	////count each person in each bracket
//	//for (auto obj : playerVec) {
//	//	//if else if statements because we only care about bronze/silver/gold/plat etc not each division inside
//	//	if (obj->GetMmr() < 600) { //bronze
//	//		++numberOfPlayersDivision.at(0).second;
//	//	}
//	//	else if (obj->GetMmr() < 1100) { //silver
//	//		++numberOfPlayersDivision.at(1).second;
//	//	}
//	//	else if (obj->GetMmr() < 1500) { //gold
//	//		++numberOfPlayersDivision.at(2).second;
//	//	}
//	//	else if (obj->GetMmr() < 2000) { //plat
//	//		++numberOfPlayersDivision.at(3).second;
//	//	}
//	//	else if (obj->GetMmr() < 2500) { //diamond
//	//		++numberOfPlayersDivision.at(4).second;
//	//	}
//	//	else if (obj->GetMmr() < 2800) { //masters
//	//		++numberOfPlayersDivision.at(5).second;
//	//	}
//	//	else if (obj->GetMmr() < 3000) { //challenger
//	//		++numberOfPlayersDivision.at(6).second;
//	//	}
//	//}
//
//
//	//for (auto i : numberOfPlayersDivision) {
//	//	std::cout << std::setw(13) << std::setfill(' ') << std::left << i.first + ": " << 
//	//		std::right << std::to_string(i.second) << ' ' + std::to_string((i.second / static_cast<double>(PLAYER_BASE)) * 100) << std::endl;
//	//	sum += i.second;
//	//}
//
//	//std::cout << std::endl << "Sum of Players: " << std::to_string(sum) << std::endl;
//
//
//	/*std::cout << std::endl;
//
//	std::thread threadCounter(counter);
//
//	threadCounter.join();*/
//
//	//system("pause");
//	return 0;
//}  


//OLD STUFF FROM TESTING PINVOKE/CALLBACKS

//extern "C" __declspec(dllexport) void CallbackAssign(foo pickle, updatePlayers playersUpdate) {
//	test = pickle;
//
//	test();
//
//	return;
//}
//
//extern "C" __declspec(dllexport) int* PlayerDistro() {
//
//	int* test = new int[8];
//	int sum = 0;
//
//	run();
//
//	for (int i = 0; i < 28; ++i) {
//		test[i] = numberOfPlayersDivisionp[i];
//		sum += numberOfPlayersDivision.at(i).second;
//	}
//
//	test[7] = sum;
//
//	return test;
//}
//
//extern "C" __declspec(dllexport) void DeleteArray(int* pArray) {
//	delete[] pArray;
//}