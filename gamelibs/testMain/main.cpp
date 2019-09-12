#include "TKMLib.h"
#include <iostream>
#include "Unit.h"

using namespace tkm;

int main() {
    string a;
    auto pUnit = Unit::createObject();
    cout << pUnit->m_baseAtk;
    getchar();
}