#pragma once

#pragma region "Control及其迭代子类相同部分的声明 NODETYPE_COMMON_PART_DECLARATION"

#define NODETYPE_COMMON_PART_DECLARATION_BEGIN(NodeType)                            \
public:                                                                             \
    using Self = NodeType;                                                          \
    static void initialize();                                                       \
    static void destroy();                                                          \
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
            if (tmp < (int)s_resPool_##NodeType.size() - 1) {                       \
                s_resPool_##NodeType[tmp] = this;                                   \
            } else {                                                                \
                for (; tmp >= (int)s_resPool_##NodeType.size() - 1;) {              \
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
    vector<NodeType*> NodeType::s_resPool_##NodeType(poolInit);                                                                     \
    int NodeType::s_resPoolEnd_##NodeType = -1;                                                                                     \
    int NodeType::s_resPoolLimit_##NodeType = poolLimit;                                                                            \
                                                                                                                                    \
    void NodeType::destroy() {                                                                                                      \
        NodeType::destroyFunc();                                                                                                    \
        NodeType::releaseResPool();                                                                                                 \
    }                                                                                                                               \
                                                                                                                                    \
    void NodeType::initialize() {                                                                                                   \
        initResPoll();                                                                                                              \
        NodeManager::getInstance()->m_mapClassTestData.insert(make_pair( string(typeid(NodeType).name()),                           \
            NodeManager::ClassTestData{NodeType::getCountObject, NodeType::getSizeObject, NodeType::getResPoolCount}));             \

#define NODETYPE_COMMON_PART_DEFINITION_END                                                                                         \
}                                                                                                                                   \


#pragma endregion


#pragma region "词典"
#define WordRowRegisterW(ENAME, PREFIX, NAME)               \
m_arr##ENAME[PREFIX##NAME] = #NAME;                         \

#define WordRowRegisterWNO(ENAME, PREFIX, NAME)             \
    PREFIX##NAME,                                           \

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
