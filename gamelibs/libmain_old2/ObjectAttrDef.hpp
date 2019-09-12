#pragma once
#include "AttrSetting.h"

template<typename DataType, AttrDataType_e adt, typename pGetFuncType>
inline int ObjectBase::getAttrValue(AttrSetting* pAs, DataType& retValue) {
    int ret = dealAttrValueFromAttrData(pAs, adt);
    if (ret >= 0) {
        retValue = pAs->getFuncDef<DataType, pGetFuncType>(this);
    }
    return ret;
}

template<typename DataType, AttrDataType_e adt, typename pGetFuncType>
inline int ObjectBase::getAttrValue(const wstring& attrName, DataType& retValue) {
    const auto& pairAttrType = DataManager::getInstance()->m_mapAttrSetting.find(attrName.hashCode());
    if (pairAttrType == DataManager::getInstance()->m_mapAttrSetting.end()) {
        /*没有找到该属性名。*/
        return -3;
    }
    return getAttrValue<DataType, adt, pGetFuncType>(pairAttrType->second, retValue);
}

template<typename DataType, AttrDataType_e adt, typename pSetFuncType>
inline int ObjectBase::setAttrValue(const wstring& attrName, const DataType& value) {
    const auto& pairAttrType = DataManager::getInstance()->m_mapAttrSetting.find(attrName.hashCode());
    if (pairAttrType == DataManager::getInstance()->m_mapAttrSetting.end()) {
        /*没有找到该属性名。*/
        return -3;
    }
    return setAttrValue<DataType, adt, pSetFuncType>(pairAttrType->second, value);
}
