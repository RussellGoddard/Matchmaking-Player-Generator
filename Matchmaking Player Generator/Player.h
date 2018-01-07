#pragma once


//forward declare dependancies
int GetRandom(int min, int max); //from GetRandom.h

//include dependancies
#include <string>
#include <utility>

//variables
const int NUMBER_OF_BRACKETS = 27;

//numbers are from my ranked distribution excel spreadsheet
//mmr values for each bracket

const std::pair<int, int>  BRONZE5 = std::make_pair(0, 199);
const std::pair<int, int>  BRONZE4 = std::make_pair(200, 299);
const std::pair<int, int>  BRONZE3 = std::make_pair(300, 399);
const std::pair<int, int>  BRONZE2 = std::make_pair(400, 499);
const std::pair<int, int>  BRONZE1 = std::make_pair(500, 599);
const std::pair<int, int>  SILVER5 = std::make_pair(600, 699);
const std::pair<int, int>  SILVER4 = std::make_pair(700, 799);
const std::pair<int, int>  SILVER3 = std::make_pair(800, 899);
const std::pair<int, int>  SILVER2 = std::make_pair(900, 999);
const std::pair<int, int>  SILVER1 = std::make_pair(1000, 1099);
const std::pair<int, int>  GOLD5 = std::make_pair(1100, 1199);
const std::pair<int, int>  GOLD4 = std::make_pair(1100, 1199);
const std::pair<int, int>  GOLD3 = std::make_pair(1200, 1299);
const std::pair<int, int>  GOLD2 = std::make_pair(1300, 1399);
const std::pair<int, int>  GOLD1 = std::make_pair(1400, 1499);
const std::pair<int, int>  PLAT5 = std::make_pair(1500, 1599);
const std::pair<int, int>  PLAT4 = std::make_pair(1600, 1699);
const std::pair<int, int>  PLAT3 = std::make_pair(1700, 1799);
const std::pair<int, int>  PLAT2 = std::make_pair(1800, 1899);
const std::pair<int, int>  PLAT1 = std::make_pair(1900, 1999);
const std::pair<int, int>  DIAMOND5 = std::make_pair(2000, 2099);
const std::pair<int, int>  DIAMOND4 = std::make_pair(2100, 2199);
const std::pair<int, int>  DIAMOND3 = std::make_pair(2200, 2299);
const std::pair<int, int>  DIAMOND2 = std::make_pair(2300, 2399);
const std::pair<int, int>  DIAMOND1 = std::make_pair(2400, 2499);
const std::pair<int, int>  MASTERS = std::make_pair(2500, 2799);
const std::pair<int, int>  CHALLENGER = std::make_pair(2800, 2999);

const std::pair<int, int> MMR_BRACKETS[27] = {BRONZE5, BRONZE4, BRONZE3, BRONZE2, BRONZE1, 
	SILVER5, SILVER4, SILVER3, SILVER2, SILVER1, GOLD5, GOLD4, GOLD3, GOLD2, GOLD1,
	PLAT5, PLAT4, PLAT3, PLAT2, PLAT1, DIAMOND5, DIAMOND4, DIAMOND3, DIAMOND2, DIAMOND1, MASTERS, CHALLENGER};

//class
//player contains id (read only), name, mmr
class Player {
public:
	//id related
	const int GetId();
	//name related
	const std::string GetName();
	void SetName(std::string);
	//mmr related
	const int GetMmr();
	void SetMmr(int );
	//constructors
	Player();
private:
	int id; //unique for each player
	std::string name; //currently just Player(#id)
	int mmr;
};
