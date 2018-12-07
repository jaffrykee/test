#include "AttrSetting.h"
#include "DataHeaders.h"

NODETYPE_COMMON_PART_DEFINITION_BEGIN(AttrSetting, 200, 500);
#pragma region "ÊôÐÔ×¢²á"
#pragma endregion
NODETYPE_COMMON_PART_DEFINITION_END

void tkm::AttrSetting::assignBase(AttrSetting* pSelf, const AttrType_e& attrType, const wstring& attrName,
    const AttrDataType_e& dataType, const NodeType_e& nodeType, const wstring& subType, bool isEnum) {
    pSelf->m_attrType = attrType;
    pSelf->m_attrName = attrName;
    pSelf->m_dataType = dataType;
    pSelf->m_nodeType = nodeType;
    pSelf->m_isEnum = isEnum;
    pSelf->m_pSubType = &subType;
    DataManager::getInstance()->m_arrNodeTypeSetting[nodeType]->m_lmapAttrSetting.insert(pair<wstring, AttrSetting*>(attrName, pSelf));
}
