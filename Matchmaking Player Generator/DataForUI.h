#pragma once

class DataForUi {
public:
	int getValue();
	void setValue(int value);

	DataForUi(int value);
	DataForUi();
private:
	int test;
	int playerDistroArr[7];
};

