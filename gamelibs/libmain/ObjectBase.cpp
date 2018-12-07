#include "ObjectBase.h"
#include "ObjectAttrDef.hpp"
#include "DataHeaders.h"

#include "NodeManager.h"
#include "StringManager.h"

NODETYPE_COMMON_PART_DEFINITION_BEGIN(ObjectBase, 0, 0);
#pragma region "属性注册"
//NODEBASE_ATTR_REGISTER(L"ssueId", SsueId, ObjectBase, HS64);
#pragma endregion
NODETYPE_COMMON_PART_DEFINITION_END

ObjectBase::ObjectBase() {
    createSelf();
}

void ObjectBase::createSelf() {

}

void ObjectBase::disposeSelf() {
}

bool tkm::ObjectBase::checkValidity(AttrSetting* pAs, AttrDataType_e dataType) {
    if (pAs->m_dataType == dataType || pAs->m_dataType == ADT_HU32 && dataType == ADT_U32
        || pAs->m_dataType == ADT_HS64 && dataType == ADT_S64) {
        return true;
    }
    return false;
}

int tkm::ObjectBase::dealAttrValueFromAttrData(AttrSetting* pAs, AttrDataType_e dataType) {
    if (!checkValidity(pAs, dataType)) {
        //类型不符
        return -1;
    }
    if (is(pAs->m_nodeType) == false) {
        //这个属性不是此节点类或迭代父类的。
        return -2;
    }
    return 0;
}

void ObjectBase::regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
    const NodeType_e& nodeType, ObjectBase::GetFuncB2_t getFunc, ObjectBase::SetFuncB2_t setFunc, const wstring& subType, bool isEnum) {
    DataManager::getInstance()->m_arrAttrSetting[attrType] = AttrSetting::createObject(attrType, attrName, dataType, nodeType, getFunc, setFunc, subType, isEnum);
    DataManager::getInstance()->regNodeAttrs(nodeType, attrType);
}

void ObjectBase::regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
    const NodeType_e& nodeType, ObjectBase::GetFuncS32_t getFunc, ObjectBase::SetFuncS32_t setFunc, const wstring& subType, bool isEnum) {
    DataManager::getInstance()->m_arrAttrSetting[attrType] = AttrSetting::createObject(attrType, attrName, dataType, nodeType, getFunc, setFunc, subType, isEnum);
    DataManager::getInstance()->regNodeAttrs(nodeType, attrType);
}

void ObjectBase::regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
    const NodeType_e& nodeType, ObjectBase::GetFuncU32_t getFunc, ObjectBase::SetFuncU32_t setFunc, const wstring& subType, bool isEnum) {
    DataManager::getInstance()->m_arrAttrSetting[attrType] = AttrSetting::createObject(attrType, attrName, dataType, nodeType, getFunc, setFunc, subType, isEnum);
    DataManager::getInstance()->regNodeAttrs(nodeType, attrType);
}

void ObjectBase::regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
    const NodeType_e& nodeType, ObjectBase::GetFuncF32_t getFunc, ObjectBase::SetFuncF32_t setFunc, const wstring& subType, bool isEnum) {
    DataManager::getInstance()->m_arrAttrSetting[attrType] = AttrSetting::createObject(attrType, attrName, dataType, nodeType, getFunc, setFunc, subType, isEnum);
    DataManager::getInstance()->regNodeAttrs(nodeType, attrType);
}

void ObjectBase::regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
    const NodeType_e& nodeType, ObjectBase::GetFuncS64_t getFunc, ObjectBase::SetFuncS64_t setFunc, const wstring& subType, bool isEnum) {
    DataManager::getInstance()->m_arrAttrSetting[attrType] = AttrSetting::createObject(attrType, attrName, dataType, nodeType, getFunc, setFunc, subType, isEnum);
    DataManager::getInstance()->regNodeAttrs(nodeType, attrType);
}

void ObjectBase::regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
    const NodeType_e& nodeType, ObjectBase::GetFuncString_t getFunc, ObjectBase::SetFuncString_t setFunc, const wstring& subType, bool isEnum) {
    DataManager::getInstance()->m_arrAttrSetting[attrType] = AttrSetting::createObject(attrType, attrName, dataType, nodeType, getFunc, setFunc, subType, isEnum);
    DataManager::getInstance()->regNodeAttrs(nodeType, attrType);
}

void ObjectBase::regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
    const NodeType_e& nodeType, ObjectBase::GetFuncWString_t getFunc, ObjectBase::SetFuncWString_t setFunc, const wstring& subType, bool isEnum) {
    DataManager::getInstance()->m_arrAttrSetting[attrType] = AttrSetting::createObject(attrType, attrName, dataType, nodeType, getFunc, setFunc, subType, isEnum);
    DataManager::getInstance()->regNodeAttrs(nodeType, attrType);
}

void tkm::ObjectBase::printData(PrintDataType_e pdt /*= PDT_Debug*/) {
    string outString;
    switch (pdt) {
    case PDT_Debug:
        debugString(outString);
        break;
    case PDT_Tree:
        treeString(outString, 0);
        break;
    default:
        debugString(outString);
        break;
    }
    printf("%s", outString.c_str());
}
