#pragma once
#include "tkmBasic.h"
#include "NodeManager.h"
#include "TKMPublic.h"

_TKMNamespaceBegin

using CCIT_e = enum ControlCompInfoType_e : char {
    CCIT_NULL = -1,
    CCIT_Panel = 0,
    CCIT_Label,
    CCIT_Button,
    CCIT_Radio,
    CCIT_Check,
    CCIT_SkillButton,
    CCIT_InputBox,
    CCIT_TextFlow,
    CCIT_Progress,
    CCIT_VirtualJoystick,
    CCIT_TimeBlock,
	CCIT_UIDrawModel,
    CCIT_ImageShape,
    CCIT_TextShape,
    CCIT_ParticleShape,
    
    //=================
    CCIT_StackPanel,
    //==================
    //==================
    CCIT_Para,
    CCIT_FlowElement,
    CCIT_Grid,
    CCIT_WrapPanel,
    CCIT_AutoGrid,
    CCIT_SlicedPanel,
    //==================
    CCIT_MAX,
};

class NodeTypeSetting;
class AttrSetting;
class DataManager {
#pragma region "数据类型"
    typedef struct CcitData_s {
        CCIT_e m_id;
        vector<NodeType_e> m_data;

        inline void addComps(NodeType_e compType, ...) {
            //http://blog.csdn.net/weiwangchao_/article/details/4857567
            va_list args;
            va_start(args, compType);
            int curComp = compType;
            if (curComp != NT_MAX) {
                m_data.push_back((NodeType_e)curComp);
                while (1) {
                    curComp = va_arg(args, int);
                    if (curComp == NT_MAX) {
                        break;
                    }
                    m_data.push_back((NodeType_e)curComp);
                }
            }
            va_end(args);
        }

        double sum(unsigned int n, ...) {
            double sum = 0;
            va_list args;//定义一个可变参数列表  
            va_start(args, n);//初始化args指向强制参数arg的下一个参数  
            while (n > 0) {
                //通过va_arg(args, double)依次获取参数的值  
                sum += va_arg(args, double);
                n--;
            }
            va_end(args);//释放args  
            return sum;
        }
    }CcitData_t;
#pragma endregion

#pragma region "静态成员"
public:
    static string s_className;
    static DataManager* s_pInstance;
#pragma endregion

#pragma region "静态方法"
public:
    inline static DataManager* getInstance() {
        if (s_pInstance == nullptr) {
            s_pInstance = new DataManager();
        }
        return s_pInstance;
    }
    static void initialize();
    static void destroy();
    static void registerReflection(int id);
#pragma endregion

#pragma region "成员"
public:
    //记录了所有NodeTypeSetting(NodeType)
    NodeTypeSetting* m_arrNodeTypeSetting[NT_MAX] = {nullptr};
    unordered_map<wstring, NodeTypeSetting*> m_mapNodeTypeSetting;
    //所有属性信息索引，index是AttrType_e
    AttrSetting* m_arrAttrSetting[AT_MAX] = {nullptr};
    unordered_map<wstrHash, AttrSetting*> m_mapAttrSetting;

    wstring m_tmpFileName;
    vector<string> m_tmpArrSplit;
#pragma endregion

#pragma region "方法"
public:
    inline DataManager() {
    }
    inline virtual ~DataManager() {
    }
    inline virtual const string& getClassName() const {
        return DataManager::s_className;
    }
public:
    void clearCache();
    void getFiles(vector<string>& arrFiles, const string& path, const string& fix, bool isIterative);
    void initTKMFunc();
    const std::bitset<NT_MAX>& getNodeInheritData(NodeType_e nt);
    ObjectBase* getInitNode(NodeType_e nodeType);

    inline void regNodeAttrs(NodeType_e nodeType, AttrType_e attrType) {

    }
#pragma endregion
};

_TKMNamespaceEnd
