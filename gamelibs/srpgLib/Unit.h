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
#pragma region "��������"
#pragma endregion

#pragma region "��̬��Ա"
#pragma endregion

#pragma region "��̬����"
#pragma endregion

#pragma region "��Ա"
public:
    int m_baseAtk;
    int m_baseHp;
#pragma endregion

#pragma region "����"
public:
#pragma endregion
};

_TKMNamespaceEnd
