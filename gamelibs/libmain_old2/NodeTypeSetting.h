#pragma once
#include "ObjectBase.h"

using namespace std;
using namespace tkm;

_TKMNamespaceBegin

class AttrSetting;
//一类属性的集合
class NodeTypeSetting : public ObjectBase {
    NODETYPE_COMMON_PART_DECLARATION_BEGIN(NodeTypeSetting);
protected:
    inline virtual void createSelf() override {
    }
    inline virtual void disposeSelf() override {
    }
    NODETYPE_COMMON_PART_DECLARATION_END(NodeTypeSetting, ObjectBase);

#pragma region "数据类型"
#pragma endregion

#pragma region "静态成员"
#pragma endregion

#pragma region "静态方法"
public:
    inline static NodeTypeSetting* createObject(NodeType_e nodeType, const wstring& name, ObjectBase* pInitNode) {
        auto pSelf = NodeTypeSetting::createObject();
        DataManager::getInstance()->m_arrNodeTypeSetting[nodeType] = pSelf;
        DataManager::getInstance()->m_mapNodeTypeSetting.insert(pair<wstring, NodeTypeSetting*>(name, pSelf));
        pSelf->m_nodeType = nodeType;
        pSelf->m_name = name;
        pSelf->m_pInitNode = pInitNode;
        auto baseType = ObjectBase::getBaseType(nodeType);
        if (baseType < NT_MAX) {
            pSelf->m_inheritData = DataManager::getInstance()->m_arrNodeTypeSetting[baseType]->m_inheritData;
        }
        pSelf->m_inheritData[nodeType] = true;
        return pSelf;
    }
#pragma endregion

#pragma region "成员"
public:
    NodeType_e m_nodeType;
    wstring m_name;
    //仅限此类的属性。
    LinkedHashMap<wstring, AttrSetting*> m_lmapAttrSetting;
    //所有属性。
    unordered_set<int> m_lhsNodeAttrs;
    //所有节点类型所对应的继承关系。（按照节点类型枚举往下排）
    std::bitset<NT_MAX> m_inheritData;
    ObjectBase* m_pInitNode = nullptr;
    //ObjectBase* m_arrInitNode[NT_MAX] = {nullptr};
#pragma endregion

#pragma region "方法"
    inline NodeTypeSetting& assign(const NodeTypeSetting& other) {
        Base::assign(other);
        return *this;
    }
    int getAttrType(const wstring& attrName) const;
#pragma endregion
};

_TKMNamespaceEnd
