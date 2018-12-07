#pragma once
#include "ObjectBase.h"

using namespace std;
using namespace tkm;

_TKMNamespaceBegin

//一类属性的集合
class AttrSetting : public ObjectBase {
    NODETYPE_COMMON_PART_DECLARATION_BEGIN(AttrSetting);
protected:
    inline virtual void createSelf() override {
    }
    inline virtual void disposeSelf() override {
    }
    NODETYPE_COMMON_PART_DECLARATION_END(AttrSetting, ObjectBase);
#pragma region "数据类型"
#pragma endregion

#pragma region "静态成员"
#pragma endregion

#pragma region "静态方法"
private:
    static void assignBase(AttrSetting* pSelf, const AttrType_e& attrType, const wstring& attrName,
        const AttrDataType_e& dataType, const NodeType_e& nodeType, const wstring& subType, bool isEnum = false);
public:
    AttrSetting_DATETYPE_DEF(B2, B2, bool);
    AttrSetting_DATETYPE_DEF(S32, S32, int);
    AttrSetting_DATETYPE_DEF(U32, U32, unsigned int);
    AttrSetting_DATETYPE_DEF(F32, float, float);
    AttrSetting_DATETYPE_DEF(S64, S64, long long);
    AttrSetting_DATETYPE_DEF(String, STR, string);
    AttrSetting_DATETYPE_DEF(WString, WSTR, wstring);
#pragma endregion

#pragma region "成员"
public:
    //属性类型
    AttrType_e m_attrType = (AttrType_e)0;
    //所归属的节点类型
    NodeType_e m_nodeType = (NodeType_e)0;
    //属性的数据类型
    AttrDataType_e m_dataType = (AttrDataType_e)0;
    bool m_isEnum = false;
    const wstring* m_pSubType = nullptr;
    //属性名
    wstring m_attrName;
    union {
        ObjectBase::GetFuncB2_t m_pGetFuncB2;
        ObjectBase::GetFuncS32_t m_pGetFuncS32;
        ObjectBase::GetFuncU32_t m_pGetFuncU32;
        ObjectBase::GetFuncF32_t m_pGetFuncfloat;
        ObjectBase::GetFuncS64_t m_pGetFuncS64;
        ObjectBase::GetFuncString_t m_pGetFuncSTR;
        ObjectBase::GetFuncWString_t m_pGetFuncWSTR;
    };
    union {
        ObjectBase::SetFuncB2_t m_pSetFuncB2;
        ObjectBase::SetFuncS32_t m_pSetFuncS32;
        ObjectBase::SetFuncU32_t m_pSetFuncU32;
        ObjectBase::SetFuncF32_t m_pSetFuncfloat;
        ObjectBase::SetFuncS64_t m_pSetFuncS64;
        ObjectBase::SetFuncString_t m_pSetFuncSTR;
        ObjectBase::SetFuncWString_t m_pSetFuncWSTR;
    };
#pragma endregion

#pragma region "方法"
    inline AttrSetting& assign(const AttrSetting& other) {
        Base::assign(other);
        return *this;
    }
public:
    template<typename DataType, typename pGetFuncType>
    inline DataType getFuncDef(ObjectBase* pObj) {
        return ((pObj)->*((pGetFuncType)m_pGetFuncB2))();
    }

    template<typename DataType, typename pSetFuncType>
    inline void setFuncDef(ObjectBase* pObj, DataType value) {
        ((pObj)->*((pSetFuncType)m_pSetFuncB2))(value);
    }
#pragma endregion
};

_TKMNamespaceEnd
