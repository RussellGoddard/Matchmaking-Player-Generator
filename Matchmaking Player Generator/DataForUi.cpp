#include "DataForUI.h"


DataForUi::DataForUi(int value) : test(value) {

}

DataForUi::DataForUi() {
	test = -1;
}


int DataForUi::getValue() {
	return test;
}

void DataForUi::setValue(int value) {
	test = value;
}