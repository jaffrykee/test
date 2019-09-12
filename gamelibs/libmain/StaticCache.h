#pragma once
#include "TKMPublic.h"

using namespace std;
using namespace tkm;

_TKMNamespaceBegin

template<typename T, int WB>
class StaticCache {
private:
    T m_data[1 << (WB + 1)];
    union {
        struct {
            unsigned long long m_curIndex : WB;
        };
        //是几都无所谓
        unsigned long long m_initData;
    };
public:
    inline T& nextData() {
        m_curIndex++;
        return m_data[m_curIndex];
    }
    inline T& curData() {
        return m_data[m_curIndex];
    }
    inline T& data(int index) {
        return m_data[index];
    }
    inline T& dData(int index) {
        return m_data[m_curIndex + index];
    }
};

_TKMNamespaceEnd
