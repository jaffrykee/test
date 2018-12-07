#include "NodeTypeSetting.h"
#include "AttrSetting.h"

NODETYPE_COMMON_PART_DEFINITION_BEGIN(NodeTypeSetting, 100, 200);
#pragma region "ÊôĞÔ×¢²á"
#pragma endregion
NODETYPE_COMMON_PART_DEFINITION_END

int NodeTypeSetting::getAttrType(const wstring& attrName) const {
    auto pairAttr = m_lmapAttrSetting.find(attrName);
    if (pairAttr != m_lmapAttrSetting.end()) {
        return pairAttr->second->m_attrType;
    }
    return -1;
}
