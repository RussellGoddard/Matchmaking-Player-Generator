#pragma once

//forward declare dependancies


//include dependancies
#include <random>
#include <stdexcept>

//class
//function to return a random integer based on a given min and max range
//seed random generator
std::random_device rand_dev;
std::mt19937 generator(rand_dev());

int GetRandom(int min, int max)
{
	if (min > max) {
		throw std::invalid_argument("min is greater than max");
	}

	int random = 0; //initialize an int for us to return
	std::uniform_int_distribution<int> distr(min, max);
	random = distr(generator);

	return random;
}