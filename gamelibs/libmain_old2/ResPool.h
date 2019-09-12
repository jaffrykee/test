#pragma once
#include <stdlib.h>
#include "tkmBasic.h"

using namespace std;
using namespace tkm;

_TKMNamespaceBegin

template <class ResType>
class ResPool {
#pragma region "数据类型"
#pragma endregion

#pragma region "静态成员"
#pragma endregion

#pragma region "静态方法"
#pragma endregion

#pragma region "成员"
private:
    vector<ResType*> m_resPool;
    int m_endIndex = -1;
    int m_limitSize;
    int m_initSize;
#pragma endregion

#pragma region "方法"
public:
    inline ResPool(int initSize = 100, int limitSize = 200) : m_resPool(initSize), m_limitSize(limitSize), m_initSize(initSize) {
        initResPoll();
    }
    inline ~ResPool() {
        releaseResPool();
    }
    inline void initResPoll() {
        for (auto& pNode : m_resPool) {
            pNode = new ResType();
        }
        m_endIndex = m_resPool.size() - 1;
    }
    inline void releaseResPool() {
        for (int i = 0; i <= m_endIndex; i++) {
            delete m_resPool[i];
        }
        m_resPool.clear();
    }
    inline int getResPoolCount() {
        return ResType::m_resPool.size();
    }
    inline ResType* createObject(void(*pCreateFunc)(ResType*) = nullptr) {
        if (m_endIndex >= 0) {
            auto& pNode = m_resPool[m_endIndex];
            if (pCreateFunc != nullptr) {
                pCreateFunc(pNode);
            }
            m_endIndex--;
            return pNode;
        } else {
            return new ResType();
        }
    }
    inline void releaseObject(ResType*& pSelf, void (*pReleaseFunc)(ResType*) = nullptr) {
        if (pSelf == nullptr) {
            return;
        }
        if (m_endIndex < m_limitSize - 1) {
            auto tmp = ++m_endIndex;
            if (pReleaseFunc != nullptr) {
                pReleaseFunc(pSelf);
            }
            if (tmp < m_resPool.size() - 1) {
                m_resPool[tmp] = pSelf;
            } else {
                for (; tmp >= m_resPool.size() - 1;) {
                    m_resPool.push_back(nullptr);
                }
                m_resPool[tmp] = pSelf;
            }
        } else {
            delete pSelf;
        }
        pSelf = nullptr;
    }
#pragma endregion
};

_TKMNamespaceEnd
