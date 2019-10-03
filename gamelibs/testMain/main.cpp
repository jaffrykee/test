#include "TKMLib.h"
#include <iostream>
#include <sstream>
#include "Unit.h"

using namespace tkm;

int main() {
    NodeManager::getInstance()->initialize();
    Unit::initialize();



    string a;
    auto pUnit = Unit::createObject();
    stringstream ss;
    ss << pUnit->m_baseAtk;
    ss >> a;
    a.append("\n");
    printf(a.c_str());
    NodeManager::getInstance()->showObjectCount();
    getchar();
}
