#pragma once
/*
    大白的标记：始于2017-01-11
*/
#include <stdlib.h>
#include "tkmBasic.h"

using namespace std;
using namespace tkm;

_TKMNamespaceBegin

class Control;
class ObjectBase;
class RenderCacheBase;
//一个静态类
class NodeManager {
#pragma region "数据类型"
public:
    typedef int(*GetCountFunc_t)();
    struct ClassTestData {
        GetCountFunc_t m_pFuncGetCountObject;
        GetCountFunc_t m_pFuncGetSizeObject;
        GetCountFunc_t m_pFuncGetResPoolCount;
    };
#pragma endregion

#pragma region "静态成员"
public:
    static string s_className;
    static NodeManager* s_pInstance;
#pragma endregion

#pragma region "静态方法"
public:
    inline static NodeManager* getInstance() {
        if (s_pInstance == nullptr) {
            s_pInstance = new NodeManager();
        }
        return s_pInstance;
    }
    static void initialize();
    static void destroy();
    static void registerReflection(int id);
#pragma endregion

#pragma region "成员"
public:
    unordered_map<string, ClassTestData> m_mapClassTestData;
    unordered_map<int, string> m_mapLanguageIdName;
    unordered_map<int, string> m_mapThemeIdName;
    unordered_map<string, int> m_mapLanguageNameId;
    unordered_map<string, int> m_mapThemeNameId;
    const int mc_initCacheCount = 1000;
    float m_lastGrayValue = 0.0f;
#ifdef _WIN32
    float m_outLineWidth = 2.f;
    float m_borderWidth = 1.f;
#endif // _WIN32
    bool m_isReady = false;
#pragma endregion

#pragma region "方法"
public:
    inline NodeManager() {
    }
    inline virtual ~NodeManager() {
    }
    inline virtual const string& getClassName() const {
        return NodeManager::s_className;
    }
public:
    template <typename srcType>
    inline static void* getVoidPtr(const srcType& srcPtr) {
        union {
            srcType _srcPtr;
            void* _dstPtr;
        }utPtr;
        utPtr._srcPtr = srcPtr;
        return utPtr._dstPtr;
    }
    void createFast();
    void showObjectCount();
#pragma endregion
};

_TKMNamespaceEnd
