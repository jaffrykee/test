#pragma once
#include "TKMLib.h"
#include "ObjectBase.h"

_TKMNamespaceBegin

class Unit : public ObjectBase {
    NODETYPE_COMMON_PART_DECLARATION_BEGIN(Unit)
protected:
    inline virtual void createSelf() override {
    }
    inline virtual void disposeSelf() override {
    }
    NODETYPE_COMMON_PART_DECLARATION_END(Unit, ObjectBase);
#pragma region "数据类型"
#pragma endregion

#pragma region "静态成员"
#pragma endregion

#pragma region "静态方法"
#pragma endregion

#pragma region "成员"
public:
    int m_baseAtk;
    int m_baseHp;
#pragma endregion

#pragma region "方法"
public:
#pragma endregion
};

_TKMNamespaceEnd
