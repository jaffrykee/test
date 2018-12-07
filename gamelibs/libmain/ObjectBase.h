#pragma once
////#include "SortedSet.h"
#include "tkmBasic.h"
#include "NodeManager.h"
#include "DataManager.h"
#include "ResPoolManager.h"
#include "StringManager.h"

using namespace std;
using namespace tkm;

_TKMNamespaceBegin
class EventNodeBase;
class AttrSetting;
class NodeTypeSetting;
class ObjectBase {
    NODETYPE_COMMON_PART_DECLARATION_BEGIN(ObjectBase)

#pragma region "数据类型"
    typedef bool(ObjectBase::*GetFuncB2_t)() const;
    typedef void (ObjectBase::*SetFuncB2_t)(bool value);
    typedef int(ObjectBase::*GetFuncS32_t)() const;
    typedef void (ObjectBase::*SetFuncS32_t)(int value);
    typedef unsigned int(ObjectBase::*GetFuncU32_t)() const;
    typedef void (ObjectBase::*SetFuncU32_t)(unsigned int value);
    typedef float(ObjectBase::*GetFuncF32_t)() const;
    typedef void (ObjectBase::*SetFuncF32_t)(float value);
    typedef long long(ObjectBase::*GetFuncS64_t)() const;
    typedef void (ObjectBase::*SetFuncS64_t)(long long value);
    typedef const string& (ObjectBase::*GetFuncString_t)() const;
    typedef void (ObjectBase::*SetFuncString_t)(const string& value);
    typedef const wstring& (ObjectBase::*GetFuncWString_t)() const;
    typedef void (ObjectBase::*SetFuncWString_t)(const wstring& value);
    typedef ObjectBase* (ObjectBase::*GetFuncClass_t)() const;
    typedef void (ObjectBase::*SetFuncClass_t)(ObjectBase* value);
#pragma endregion

#pragma region "静态成员"
#pragma endregion

#pragma region "静态方法"
public:
    inline static ObjectBase* getInitNode(NodeType_e nodeType) {
        return DataManager::getInstance()->getInitNode(nodeType);
    }
    inline static ObjectBase* createObject(NodeType_e nodeType) {
        return getInitNode(nodeType)->createCopy();
    }
    //看objType是不是baseType的迭代子类。
    inline static bool is(NodeType_e nodeType, NodeType_e baseType) {
        return getInitNode(nodeType)->is(baseType);
    }
    inline static NodeType_e getBaseType(NodeType_e childType) {
        return getInitNode(childType)->getBaseType();
    }
    inline static void destroyFunc() {

    }
#pragma endregion

#pragma region "成员"
#pragma endregion

#pragma region "方法"
    //ObjectBase获取和设置属性通用函数定义
public:
    template<typename DataType, AttrDataType_e adt, typename pGetFuncType>
    int getAttrValue(AttrSetting* pAs, DataType& retValue);

    template<typename DataType, AttrDataType_e adt, typename pGetFuncType>
    int getAttrValue(const wstring& attrName, DataType& retValue);

    template<typename DataType, AttrDataType_e adt, typename pSetFuncType>
    int setAttrValue(const wstring& attrName, const DataType& value);

    template <class TGetFunc, class TSetFunc>
    void regAttrFunc(const AttrType_e& attrType, const wstring& attrName,
        const AttrDataType_e& dataType, const NodeType_e& nodeType, TGetFunc getFunc, TSetFunc setFunc, const wstring& subType, bool isEnum = false) {
        using Type = typename remove_reference<typename function_traits<TGetFunc>::return_type>::Type;
        using GetFunc = typename MemberFuncTrait<Type>::GetFunc;
        using SetFunc = typename MemberFuncTrait<Type>::SetFunc;
        static_assert(!type_equal<GetFunc, void>::value, "property type not support!");
        static_assert(function_traits<TGetFunc>::isConst, "get function must be const!");
        regAttrSetting(attrType, attrName, dataType, nodeType, (GetFunc)getFunc, (SetFunc)setFunc, subType, isEnum);
    }
    template <class TGetFunc, class TSetFunc>
    void regAttrFunc(const AttrType_e& attrType, const wstring& attrName,
        const AttrDataType_e& dataType, const NodeType_e& nodeType, TGetFunc getFunc, TSetFunc setFunc, AttrSubType_e subType = AST_halfNormal, bool isEnum = false) {
        using Type = typename remove_reference<typename function_traits<TGetFunc>::return_type>::Type;
        using GetFunc = typename MemberFuncTrait<Type>::GetFunc;
        using SetFunc = typename MemberFuncTrait<Type>::SetFunc;
        static_assert(!type_equal<GetFunc, void>::value, "property type not support!");
        static_assert(function_traits<TGetFunc>::isConst, "get function must be const!");
        regAttrSetting(attrType, attrName, dataType, nodeType, (GetFunc)getFunc, (SetFunc)setFunc, DictionaryManager::getInstance()->m_arrAttrSubType[subType], isEnum);
    }

//     OBJECTBASE_ATTRFUNC_DECLARATION(int, S32);
//     OBJECTBASE_ATTRFUNC_DECLARATION(unsigned int, U32);
//     OBJECTBASE_ATTRFUNC_DECLARATION(long long, S64);
//     OBJECTBASE_ATTRFUNC_DECLARATION(bool, B2);
//     OBJECTBASE_ATTRFUNC_DECLARATION(float, float);
//     OBJECTBASE_ATTRFUNC_DECLARATION(double, double);
//     OBJECTBASE_ATTRFUNC_DECLARATION(string, STR);
//     OBJECTBASE_ATTRFUNC_DECLARATION(wstring, WSTR);

protected:
    virtual void createSelf();
    virtual void disposeSelf();
public:
    //把other赋给this。
    inline ObjectBase& assign(const ObjectBase& other) {
        return *this;
    }
    inline virtual void assign(ObjectBase* pSrc) {
        ObjectBase::assign(*pSrc);
    }
    inline virtual void create() {
        ObjectBase::createSelf();
    }
    inline virtual void assignInitNode() {
        this->create();
    }
    inline virtual void dispose() {
        ObjectBase::disposeSelf();
    }
    ObjectBase();
    inline virtual ~ObjectBase() {
        ObjectBase::disposeSelf();
    }
    inline virtual NodeType_e getBaseType() const {
        return NT_MAX;
    }
    inline virtual const string& getBaseClassName() const {
        return StringManager::getInstance()->mc_strEmpty;
    }
    inline virtual const wstring& getBaseClassNameW() const {
        return StringManager::getInstance()->mc_wstrEmpty;
    }
    inline virtual bool enableUserAttr() const {
        return false;
    }
public:
    inline ObjectBase* createCopy() {
        auto pCopy = createCurObject();
        pCopy->assign(this);
        return pCopy;
    }
public:
    //判断这个对象是不是这种类型，或者它的迭代子类型。
    inline bool is(NodeType_e type) const {
        return getNodeInheritData()[type];
    }
    bool checkValidity(AttrSetting* pAs, AttrDataType_e dataType);
    int dealAttrValueFromAttrData(AttrSetting* pAs, AttrDataType_e dataType);

    void regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
        const NodeType_e& nodeType, ObjectBase::GetFuncB2_t getFunc, ObjectBase::SetFuncB2_t setFunc, const wstring& subType, bool isEnum = false);
    void regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
        const NodeType_e& nodeType, ObjectBase::GetFuncS32_t getFunc, ObjectBase::SetFuncS32_t setFunc, const wstring& subType, bool isEnum = false);
    void regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
        const NodeType_e& nodeType, ObjectBase::GetFuncU32_t getFunc, ObjectBase::SetFuncU32_t setFunc, const wstring& subType, bool isEnum = false);
    void regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
        const NodeType_e& nodeType, ObjectBase::GetFuncF32_t getFunc, ObjectBase::SetFuncF32_t setFunc, const wstring& subType, bool isEnum = false);
    void regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
        const NodeType_e& nodeType, ObjectBase::GetFuncS64_t getFunc, ObjectBase::SetFuncS64_t setFunc, const wstring& subType, bool isEnum = false);
    void regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
        const NodeType_e& nodeType, ObjectBase::GetFuncString_t getFunc, ObjectBase::SetFuncString_t setFunc, const wstring& subType, bool isEnum = false);
    void regAttrSetting(const AttrType_e& attrType, const wstring& attrName, const AttrDataType_e& dataType,
        const NodeType_e& nodeType, ObjectBase::GetFuncWString_t getFunc, ObjectBase::SetFuncWString_t setFunc, const wstring& subType, bool isEnum = false);

public:
    virtual void debugString(string& outString) {}
    virtual void treeString(string& outString, int step) {}
    virtual void printData(PrintDataType_e pdt = PDT_Debug);
#pragma endregion
};

_TKMNamespaceEnd
