#pragma once

#pragma region "Control及其迭代子类相同部分的声明 NODETYPE_COMMON_PART_DECLARATION"

#define NODETYPE_COMMON_PART_DECLARATION_BEGIN(NodeType)                            \
public:                                                                             \
    using Self = NodeType;                                                          \
protected:                                                                          \
    /*节点类型*/                                                                     \
    static const NodeType_e sc_nodeType;                                            \
    /*节点类型名*/                                                                   \
    static wstring s_nodeTypeName;                                                  \
    static string s_className;                                                      \
    static wstring s_classNameW;                                                    \
    static wstring s_classNamePreW;                                                 \
public:                                                                             \
    static NodeType* s_pInitNode;                                                   \
public:                                                                             \
    inline virtual const string& getClassName() const {                             \
        return NodeType::s_className;                                               \
    }                                                                               \
    inline virtual const string& getCurClassName() {                                \
        return NodeType::s_className;                                               \
    }                                                                               \
    inline static const wstring& getClassNameW() {                                  \
        return NodeType::s_classNameW;                                              \
    }                                                                               \
    inline virtual const wstring& getCurClassNameW() {                              \
        return NodeType::s_classNameW;                                              \
    }                                                                               \
    inline static const wstring& getClassNamePreW() {                               \
        return NodeType::s_classNamePreW;                                           \
    }                                                                               \
    inline virtual const wstring& getCurClassNamePreW() {                           \
        return NodeType::s_classNamePreW;                                           \
    }                                                                               \
    static void initialize();                                                       \
    static void destroy();                                                          \
public:                                                                             \
    inline virtual const NodeType_e& getNodeType() const {                          \
        return NodeType::sc_nodeType;                                               \
    }                                                                               \
    inline virtual const std::bitset<NT_MAX>& getNodeInheritData() const {          \
        return DataManager::getInstance()->getNodeInheritData(sc_nodeType);         \
    }                                                                               \
    inline virtual const wstring& getNodeTypeName() {                               \
        return NodeType::s_nodeTypeName;                                            \
    }                                                                               \
    inline virtual void initNode() {                                                \
        *this = *s_pInitNode;                                                       \
    }                                                                               \
private:                                                                            \
    static vector<NodeType*> s_resPool_##NodeType;                                  \
    static int s_resPoolEnd_##NodeType;                                             \
    static int s_resPoolLimit_##NodeType;                                           \
public:                                                                             \
    inline static void initResPoll() {                                              \
        for (auto& pNode : s_resPool_##NodeType) {                                  \
            pNode = new NodeType();                                                 \
        }                                                                           \
        s_resPoolEnd_##NodeType = s_resPool_##NodeType.size() - 1;                  \
    }                                                                               \
    inline static void releaseResPool() {                                           \
        for (int i = 0; i <= s_resPoolEnd_##NodeType; i++) {                        \
            delete s_resPool_##NodeType[i];                                         \
        }                                                                           \
        s_resPool_##NodeType.clear();                                               \
    }                                                                               \
    inline virtual NodeType* createCurObject() {                                    \
        return NodeType::createObject();                                            \
    }                                                                               \
    static int s_countObject;                                                       \
    inline static int getCountObject() {                                            \
        return s_countObject;                                                       \
    }                                                                               \
    inline static int getSizeObject() {                                             \
        return sizeof(NodeType);                                                    \
    }                                                                               \
    inline static int getResPoolCount() {                                           \
        return NodeType::s_resPool_##NodeType.size();                               \
    }                                                                               \
    inline static NodeType* createObject() {                                        \
        s_countObject++;                                                            \
        NodeType* pNode;                                                            \
        if (s_resPoolEnd_##NodeType >= 0) {                                         \
            pNode = s_resPool_##NodeType[s_resPoolEnd_##NodeType];                  \
            s_resPoolEnd_##NodeType--;                                              \
        } else {                                                                    \
            pNode = new NodeType();                                                 \
        }                                                                           \
        pNode->create();                                                            \
        return pNode;                                                               \
    }                                                                               \
    inline virtual void releaseObject() {                                           \
        s_countObject--;                                                            \
        dispose();                                                                  \
        if (s_resPoolEnd_##NodeType < s_resPoolLimit_##NodeType - 1) {              \
            auto tmp = ++s_resPoolEnd_##NodeType;                                   \
            initNode();                                                             \
            if (tmp < s_resPool_##NodeType.size() - 1) {                            \
                s_resPool_##NodeType[tmp] = this;                                   \
            } else {                                                                \
                for (; tmp >= s_resPool_##NodeType.size() - 1;) {                   \
                    s_resPool_##NodeType.push_back(nullptr);                        \
                }                                                                   \
                s_resPool_##NodeType[tmp] = this;                                   \
            }                                                                       \
        } else {                                                                    \
            delete this;                                                            \
        }                                                                           \
    }                                                                               \

#define NODETYPE_COMMON_PART_DECLARATION_MID(NodeType)                              \
protected:                                                                          \
    inline virtual void createSelf() override {                                     \
    }                                                                               \
    inline virtual void disposeSelf() override {                                    \
    }                                                                               \

#define NODETYPE_COMMON_PART_DECLARATION_END(NodeType, BaseType)                    \
public:                                                                             \
    using Base = BaseType;                                                          \
    inline NodeType() : BaseType() {                                                \
    }                                                                               \
    inline virtual ~NodeType() {                                                    \
    }                                                                               \
protected:                                                                          \
    inline virtual void create() override {                                         \
        BaseType::create();                                                         \
        NodeType::createSelf();                                                     \
    }                                                                               \
    inline virtual void dispose() override {                                        \
        NodeType::disposeSelf();                                                    \
        BaseType::dispose();                                                        \
    }                                                                               \
public:                                                                             \
    inline virtual NodeType_e getBaseType() const override {                        \
        return Base::sc_nodeType;                                                   \
    }                                                                               \
    inline virtual const string& getBaseClassName() const override {                \
        return Base::s_className;                                                   \
    }                                                                               \
    inline virtual const wstring& getBaseClassNameW() const override {              \
        return Base::s_classNameW;                                                  \
    }                                                                               \
    inline virtual void assign(ObjectBase* pSrc) override {                         \
        if (pSrc->is(NT_##NodeType)) {                                              \
            NodeType::assign(*((NodeType*)pSrc));                                   \
        } else {                                                                    \
            BaseType::assign(pSrc);                                                 \
        }                                                                           \
    }                                                                               \

#define NODETYPE_COMMON_PART_DECLARATION(NodeType, BaseType)                        \
    NODETYPE_COMMON_PART_DECLARATION_BEGIN(NodeType)                                \
    NODETYPE_COMMON_PART_DECLARATION_MID(NodeType)                                  \
    NODETYPE_COMMON_PART_DECLARATION_END(NodeType, BaseType)                        \

#pragma endregion

#pragma region "Control及其迭代子类相同部分的定义 NODETYPE_COMMON_PART_DEFINITION"

#define CONTROL_RESPOOLINIT 3000
#define CONTROL_RESPOOLLIMIT 10000

#define NODETYPE_COMMON_PART_DEFINITION_BEGIN(NodeType, poolInit, poolLimit)                                                        \
    int NodeType::s_countObject = 0;                                                                                                \
    /*节点类型名*/                                                                                                                   \
    wstring NodeType::s_nodeTypeName = L#NodeType;                                                                                   \
    vector<NodeType*> NodeType::s_resPool_##NodeType(poolInit);                                                                     \
    int NodeType::s_resPoolEnd_##NodeType = -1;                                                                                     \
    int NodeType::s_resPoolLimit_##NodeType = poolLimit;                                                                            \
    string NodeType::s_className = string("TKM") + #NodeType;                                                                      \
    wstring NodeType::s_classNameW = wstring(L"TKM") + wstring(L#NodeType);                                                          \
    wstring NodeType::s_classNamePreW = wstring(L"TKM") + wstring(L#NodeType) + L"_";                                                 \
    NodeType* NodeType::s_pInitNode = nullptr;                                                                                         \
    const NodeType_e NodeType::sc_nodeType = NT_##NodeType;                                                                         \
                                                                                                                                    \
    void NodeType::destroy() {                                                                                                      \
        NodeType::destroyFunc();                                                                                                    \
        NodeType::releaseResPool();                                                                                                 \
        safe_delete(s_pInitNode);                                                                                                   \
    }                                                                                                                               \
                                                                                                                                    \
    void NodeType::initialize() {                                                                                                   \
        NodeType::s_pInitNode = new NodeType();                                                                                     \
        NodeType::s_pInitNode->assignInitNode();                                                                                    \
        initResPoll();                                                                                                              \
        NodeManager::getInstance()->m_mapClassTestData.insert(make_pair(NodeType::s_pInitNode->getClassName(),                      \
            NodeManager::ClassTestData{NodeType::getCountObject, NodeType::getSizeObject, NodeType::getResPoolCount}));             \
        NodeTypeSetting::createObject(NT_##NodeType, s_nodeTypeName, NodeType::s_pInitNode);                                        \

#define NODETYPE_COMMON_PART_DEFINITION_END                                                                                         \
}                                                                                                                                   \


#pragma endregion

#pragma region "Control单个属性注册 NODEBASE_ATTR_REGISTER"
#define NODEBASE_ATTR_REGISTER(shortName, AttrName, nodeType, dataTypeTAG, ...)                                         \
regAttrFunc(AT_##nodeType##_##AttrName, wstring( shortName ),                                                \
    ADT_##dataTypeTAG, NT_##nodeType, &Self::get##AttrName, &Self::set##AttrName,##__VA_ARGS__);                        \

#define NODEBASE_ATTR_REGISTER_GETTER(shortName, AttrName, nodeType, dataTypeTAG, ...)                                  \
regAttrFunc(AT_##nodeType##_##AttrName, wstring( shortName ),                                                \
    ADT_##dataTypeTAG, NT_##nodeType, &Self::get##AttrName, nullptr ,##__VA_ARGS__);                                        \

#define UIComponent_ControlAttr_Def(ComponentType, AttrName, dataType)          \
dataType tkm::Control::get##AttrName() const {                                 \
    auto comp = (ComponentType*)getComponentByClass(NT_##ComponentType);        \
    if (comp) {                                                                 \
        return comp->get##AttrName();                                           \
    } else {                                                                    \
        LogPrintf("ui attr getter error...:%s", 1, #AttrName);                  \
        return dataType();                                                      \
    }                                                                           \
}                                                                               \
                                                                                \
void tkm::Control::set##AttrName(dataType value) {                             \
    auto comp = (ComponentType*)getComponentByClass(NT_##ComponentType);        \
    if (comp) {                                                                 \
        comp->set##AttrName(value);                                             \
    } else {                                                                    \
        LogPrintf("ui attr setter error...:%s", 1, #AttrName);                  \
    }                                                                           \
}                                                                               \

#define UIComponent_ControlAttr_Def_STR(ComponentType, AttrName, dataType)      \
const dataType& tkm::Control::get##AttrName() const {                          \
    static dataType returnStr = "";                                             \
    auto comp = (ComponentType*)getComponentByClass(NT_##ComponentType);        \
    if (comp) {                                                                 \
        return comp->get##AttrName();                                           \
    } else {                                                                    \
        LogPrintf("ui attr getter error...:%s", 1, #AttrName);                  \
        return returnStr;                                                       \
    }                                                                           \
}                                                                               \
                                                                                \
void tkm::Control::set##AttrName(const dataType& value) {                      \
    auto comp = (ComponentType*)getComponentByClass(NT_##ComponentType);        \
    if (comp) {                                                                 \
        comp->set##AttrName(value);                                             \
    } else {                                                                    \
        LogPrintf("ui attr setter error...:%s", 1, #AttrName);                  \
    }                                                                           \
}                                                                               \

#pragma endregion

#define CCIT_DEF(Name, ...)                                                                      \
m_ccitData[CCIT_##Name].m_id = CCIT_##Name;                                                      \
m_ccitData[CCIT_##Name].addComps(__VA_ARGS__);                                                   \
pEs = ElementSetting::createObject(pTkm, #Name, NT_Control, CCIT_##Name);                       \
m_ccitData[CCIT_##Name].m_pEs = pEs;                                                             \
DataManager::getInstance()->m_lmapCcit.insert(pEs->m_name.hashCode(), &m_ccitData[CCIT_##Name]);            \
pEtkm->addChild(pEs);                                                                           \

#pragma region "属性接口"
#define ATTR_MENUM(NodeType, attrName) AT_##NodeType##_##attrName
#define ATTR_GNAME(AttrName) get##AttrName
#define ATTR_SNAME(AttrName) set##AttrName

#define ATTR_DEF_MEMBER(attrType, member, initValue)                                                                                                    \
private:                                                                                                                                                \
    attrType member = initValue;                                                                                                                        \

#define ATTR_DEF_SUBBEG(member, attrName, AttrName, NodeType, attrType, attrPermit, getterPermit, getterType, setterPermit, setterType)                 \
getterPermit:                                                                                                                                           \
    inline getterType ATTR_GNAME(AttrName)() const {                                                                                                    \
        return member;                                                                                                                                  \
    }                                                                                                                                                   \
setterPermit:                                                                                                                                           \
    inline void ATTR_SNAME(AttrName)(setterType value) {                                                                                                \
        Event tmpEvent = Event(Event::ET_AttrValueChanged, (int)ATTR_MENUM(NodeType, attrName), 0);                                                     \
        onEvent(tmpEvent);                                                                  \

#define ATTR_DEF_SUBEND }

#define ATTR_DEF_SUBALL(member, attrName, AttrName, NodeType, attrType, attrPermit, getterPermit, getterType, setterPermit, setterType)                 \
    ATTR_DEF_SUBBEG(member, attrName, AttrName, NodeType, attrType, attrPermit, getterPermit, getterType, setterPermit, setterType)                     \
        member = value;                                                                                                                                 \
    ATTR_DEF_SUBEND                                                                                                                                     \

#define ATTR_DEF_ALLBEG(member, attrName, AttrName, NodeType, attrType, attrPermit, initValue, getterPermit, getterType, setterPermit, setterType)         \
    ATTR_DEF_MEMBER(attrType, member, initValue)                                                                                                        \
    ATTR_DEF_SUBBEG(member, attrName, AttrName, NodeType, attrType, attrPermit, getterPermit, getterType, setterPermit, setterType)                     \

#define ATTR_DEF_ALLEND }

#define ATTR_DEF_ALL(member, attrName, AttrName, NodeType, attrType, attrPermit, initValue, getterPermit, getterType, setterPermit, setterType)         \
    ATTR_DEF_MEMBER(attrType, member, initValue)                                                                                                        \
    ATTR_DEF_SUBALL(member, attrName, AttrName, NodeType, attrType, attrPermit, getterPermit, getterType, setterPermit, setterType)                     \

#pragma endregion

#pragma region "AttrSetting"
//给类型属性的构造和初值获取
#define AttrSetting_DATETYPE_DEF(Suffix1, Suffix2, DataType)                                                                        \
inline static AttrSetting* createObject(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,        \
    const NodeType_e& nodeType, ObjectBase::GetFunc##Suffix1##_t pGetFunc, ObjectBase::SetFunc##Suffix1##_t pSetFunc,                             \
    const wstring& subType = L"halfNormal", bool isEnum = false) {                                                                   \
    auto* pSelf = AttrSetting::createObject();                                                                                      \
    assignBase(pSelf, attrType, attrName, dataType, nodeType, subType, isEnum);                                                     \
    pSelf->m_pGetFunc##Suffix2 = pGetFunc;                                                                                          \
    pSelf->m_pSetFunc##Suffix2 = pSetFunc;                                                                                          \
    return pSelf;                                                                                                                   \
}                                                                                                                                   \
inline bool getDefaultValue##Suffix2(DataType& value) {                                                                             \
    auto pNode = ObjectBase::getInitNode(m_nodeType);                                                                               \
    if (pNode != nullptr) {                                                                                                         \
        value = (pNode->*m_pGetFunc##Suffix2)();                                                                                    \
        return true;                                                                                                                \
    }                                                                                                                               \
    return false;                                                                                                                   \
}                                                                                                                                   \

#pragma endregion

#pragma region "词典"
#define WordRowRegisterW(ENAME, PREFIX, NAME)               \
m_arr##ENAME[PREFIX##NAME] = #NAME;                         \

#define WordRowRegisterWNO(ENAME, PREFIX, NAME)             \
    PREFIX##NAME,                                           \

#pragma endregion

#pragma region "Node & Control"
#define ControlParser(FUNC)                                                     \
do {                                                                            \
    for (auto& pAttrInfo : childData.m_arrAttr) {                               \
        if (pAttrInfo->m_attrType == (unsigned short)AT_Control_DataCcit) {     \
            auto pCtrl = Control::createObject((CCIT_e)pAttrInfo->m_iData);     \
            FUNC(pCtrl);                                                        \
            pCtrl->setData(childData);                                          \
            ret = NT_Control;                                                   \
            break;                                                              \
        }                                                                       \
    }                                                                           \
    if (ret < 0) {                                                              \
        auto pCtrl = Control::createObject(CCIT_Panel);                         \
        FUNC(pCtrl);                                                            \
        pCtrl->setData(childData);                                              \
        ret = NT_Control;                                                       \
    }                                                                           \
} while (0)                                                                     \

#define CheckHidden(...)                                                        \
do {                                                                            \
    if (getHost() == nullptr || getHost()->getDataIsVisible() == false) {       \
        return __VA_ARGS__;                                                     \
    }                                                                           \
} while (0)                                                                     \

#define TriggerEvent(...)                                               \
onEvent(TKMEvent::createTKMEvent(__VA_ARGS__));                           \

#define ForeachComp(Func)												\
do {																	\
    for (auto& pComp : m_components) {									\
        pComp->Func;													\
    }																	\
} while (0);

#define ForeachChild(Func)												\
do {																	\
    for (auto& pComp : *this) {                                         \
        for (auto& pChild : *pComp) {                                   \
            pChild->Func;                                               \
        }                                                               \
    }                                                                   \
} while (0);                                                            \

#pragma endregion

#pragma region "Manager"
#define RPM_Release(pSelf, Container)                                       \
do {                                                                        \
    RPM::s_rp##Container.releaseObject(pSelf, RPM::release##Container);     \
} while (0)                                                                 \

#define safe_release(pObject)           \
do {                                    \
    if (pObject != nullptr) {           \
        pObject->releaseObject();       \
        pObject = nullptr;              \
    }                                   \
} while (0)                             \

#define safe_delete(pObject)            \
do {                                    \
    if (pObject != nullptr) {           \
        delete pObject;                 \
        pObject = nullptr;              \
    }                                   \
} while (0)                             \

#pragma endregion
