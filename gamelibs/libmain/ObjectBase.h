#pragma once
////#include "SortedSet.h"
#include "tkmBasic.h"
#include "NodeManager.h"
#include "ResPoolManager.h"
#include "StringManager.h"

using namespace std;
using namespace tkm;

_TKMNamespaceBegin
class EventNodeBase;
class AttrSetting;
class NodeTypeSetting;
class ObjectBase {
    NODETYPE_COMMON_PART_DECLARATION_BEGIN(ObjectBase)

#pragma region "数据类型"
    typedef bool(ObjectBase::*GetFuncB2_t)() const;
    typedef void (ObjectBase::*SetFuncB2_t)(bool value);
    typedef int(ObjectBase::*GetFuncS32_t)() const;
    typedef void (ObjectBase::*SetFuncS32_t)(int value);
    typedef unsigned int(ObjectBase::*GetFuncU32_t)() const;
    typedef void (ObjectBase::*SetFuncU32_t)(unsigned int value);
    typedef float(ObjectBase::*GetFuncF32_t)() const;
    typedef void (ObjectBase::*SetFuncF32_t)(float value);
    typedef long long(ObjectBase::*GetFuncS64_t)() const;
    typedef void (ObjectBase::*SetFuncS64_t)(long long value);
    typedef const string& (ObjectBase::*GetFuncString_t)() const;
    typedef void (ObjectBase::*SetFuncString_t)(const string& value);
    typedef const wstring& (ObjectBase::*GetFuncWString_t)() const;
    typedef void (ObjectBase::*SetFuncWString_t)(const wstring& value);
    typedef ObjectBase* (ObjectBase::*GetFuncClass_t)() const;
    typedef void (ObjectBase::*SetFuncClass_t)(ObjectBase* value);
#pragma endregion

#pragma region "静态成员"
#pragma endregion

#pragma region "静态方法"
public:
    inline static void destroyFunc() {

    }
#pragma endregion

#pragma region "成员"
#pragma endregion

#pragma region "方法"

protected:
    virtual void createSelf();
    virtual void disposeSelf();
public:
    //把other赋给this。
    inline virtual void create() {
        ObjectBase::createSelf();
    }
    inline virtual void dispose() {
        ObjectBase::disposeSelf();
    }
    ObjectBase();
    inline virtual ~ObjectBase() {
        ObjectBase::disposeSelf();
    }
#pragma endregion
};

_TKMNamespaceEnd
