#pragma once
#include "ObjectBase.h"

using namespace std;
using namespace tkm;

_TKMNamespaceBegin

class AttrSetting;
//һ�����Եļ���
class NodeTypeSetting : public ObjectBase {
    NODETYPE_COMMON_PART_DECLARATION_BEGIN(NodeTypeSetting);
protected:
    inline virtual void createSelf() override {
    }
    inline virtual void disposeSelf() override {
    }
    NODETYPE_COMMON_PART_DECLARATION_END(NodeTypeSetting, ObjectBase);

#pragma region "��������"
#pragma endregion

#pragma region "��̬��Ա"
#pragma endregion

#pragma region "��̬����"
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

#pragma region "��Ա"
public:
    NodeType_e m_nodeType;
    wstring m_name;
    //���޴�������ԡ�
    LinkedHashMap<wstring, AttrSetting*> m_lmapAttrSetting;
    //�������ԡ�
    unordered_set<int> m_lhsNodeAttrs;
    //���нڵ���������Ӧ�ļ̳й�ϵ�������սڵ�����ö�������ţ�
    std::bitset<NT_MAX> m_inheritData;
    ObjectBase* m_pInitNode = nullptr;
    //ObjectBase* m_arrInitNode[NT_MAX] = {nullptr};
#pragma endregion

#pragma region "����"
    inline NodeTypeSetting& assign(const NodeTypeSetting& other) {
        Base::assign(other);
        return *this;
    }
    int getAttrType(const wstring& attrName) const;
#pragma endregion
};

_TKMNamespaceEnd
