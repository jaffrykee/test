#pragma once
#include "tkmBasic.h"
#include "NodeManager.h"
#include "ResPool.h"
#include "StaticCache.h"

_TKMNamespaceBegin

class EventNodeBase;
class UIComponent;
class ResPoolManager {
public:
    static ResPool<vector<EventNodeBase*>> s_rpEventNode;
    static ResPool<vector<UIComponent*>> s_rpIEventComponent;
    static ResPool<vector<UIComponent*>> s_rpIDrawComponent;
    static ResPool<vector<wstring>> s_rpArrStringW;
    static ResPool<string> s_rpString;
    static ResPool<wstring> s_rpStringW;

    inline static void releaseArrStringW(vector<wstring>* pSelf) {
        if (pSelf != nullptr) {
            pSelf->clear();
        }
    }
    inline static void releaseString(string* pSelf) {
        if (pSelf != nullptr) {
            pSelf->clear();
        }
    }
    inline static void releaseStringW(wstring* pSelf) {
        if (pSelf != nullptr) {
            pSelf->clear();
        }
    }
};

using RPM = ResPoolManager;

_TKMNamespaceEnd
