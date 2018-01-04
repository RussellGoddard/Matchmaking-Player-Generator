#include "Player.h"

/*

//player contains id (read only), name, mmr
class Player {
public:
//id related
const int getId();
//name related
const std::string getName();
std::string setName();
//mmr related
const int getMmr();
int setMmr();
//constructors
Player();
private:
int id;
std::string name;
int mmr;
};
*/

//global variables
int nextId = 0; //keeps track of next available id
const std::string FIRST_NAME_PART = "Player"; //all players for now will be called "Player(ID#)"


//mmr distribution values
const int BRONZE5_DISTRO = 362;
const int BRONZE4_DISTRO = 771;
const int BRONZE3_DISTRO = 1246;
const int BRONZE2_DISTRO = 1761;
const int BRONZE1_DISTRO = 2241;
const int SILVER5_DISTRO = 3353;
const int SILVER4_DISTRO = 4219;
const int SILVER3_DISTRO = 5089;
const int SILVER2_DISTRO = 5884;
const int SILVER1_DISTRO = 6353;
const int GOLD5_DISTRO = 7503;
const int GOLD4_DISTRO = 7968;
const int GOLD3_DISTRO = 8330;
const int GOLD2_DISTRO = 8564;
const int GOLD1_DISTRO = 8922;
const int PLAT5_DISTRO = 9314;
const int PLAT4_DISTRO = 9478;
const int PLAT3_DISTRO = 9618;
const int PLAT2_DISTRO = 9726;
const int PLAT1_DISTRO = 9771;
const int DIAMOND5_DISTRO = 9923;
const int DIAMOND4_DISTRO = 9960;
const int DIAMOND3_DISTRO = 9979;
const int DIAMOND2_DISTRO = 9989;
const int DIAMOND1_DISTRO = 9994;
const int MASTERS_DISTRO = 9998;
const int CHALLENGER_DISTRO = 9999;

//array because we don't need dynamic memmory
int MMR_DISTRO[NUMBER_OF_BRACKETS] = {BRONZE5_DISTRO, BRONZE4_DISTRO, BRONZE3_DISTRO, BRONZE2_DISTRO, BRONZE1_DISTRO, 
	SILVER5_DISTRO, SILVER4_DISTRO, SILVER3_DISTRO, SILVER2_DISTRO, SILVER1_DISTRO,
	GOLD5_DISTRO, GOLD4_DISTRO, GOLD3_DISTRO, GOLD2_DISTRO, GOLD1_DISTRO,
	PLAT5_DISTRO, PLAT4_DISTRO, PLAT3_DISTRO, PLAT2_DISTRO, PLAT1_DISTRO,
	DIAMOND5_DISTRO, DIAMOND4_DISTRO, DIAMOND3_DISTRO, DIAMOND2_DISTRO, DIAMOND1_DISTRO,
	MASTERS_DISTRO, CHALLENGER_DISTRO};


//function to return a random integer based on a given min and max range
//seed random generator
std::random_device rand_dev;
std::mt19937 generator(rand_dev());

int GetRandom(int min, int max)
{
	int random = 0; //initialize an int for us to return
	std::uniform_int_distribution<int> distr(min, max);
	random = distr(generator);

	return random;
}

//randomly generate player mmr and return
int InitialMmr() {
	//find the starting mmr that this specific player will have based on distribution stated in excel spreadsheet
	int randDistro = -1;
	int startingMmr = -1;

	//first roll a random number between 0 and 9999
	randDistro = GetRandom(0, 9999);

	//find the bracket this person falls into
	//this is done by comparing randDistro to mmrDistro[], if they are <= mmrDistro[i] they fall into that bracket
	for (int i = 0; i < NUMBER_OF_BRACKETS; ++i) {
		if (randDistro <= MMR_DISTRO[i]) {
			//we have the bracket they fall into, now randomly place them inside the bracket
			startingMmr = GetRandom(MMR_BRACKETS[i].first, MMR_BRACKETS[i].second);
			break; //leave the loop
		}
	}

	return startingMmr;
}

//Player Class
//id related
const int Player::GetId() {
	return id;
}

//name related
const std::string Player::GetName() {
	return name;
}

void Player::SetName(std::string value) {
	name = value;
}

//mmr related
const int Player::GetMmr() {
	return mmr;
}

void Player::SetMmr(int value) {
	mmr = value;
}

//constructors
Player::Player() {
	id = nextId;
	name = FIRST_NAME_PART + std::to_string(nextId);
	++nextId; //increment nextId after we are done with it
	mmr = InitialMmr();
}