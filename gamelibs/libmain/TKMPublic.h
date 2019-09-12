#pragma once
#include <bitset>
#include "TKMDef.h"
#include <vector>
#include <map>
#include <unordered_map>
#include <unordered_set>
#include <comutil.h>
#include "LinkedHashMap.h"
#pragma comment(lib, "comsuppw.lib")

#define _TKMNamespaceBegin namespace tkm {
#define _TKMNamespaceEnd   }

using namespace std;

#define TKM_FTMAX 9999999.f
#define TKM_DP 0.001f
#define TKM_DPH 0.001f
#define TKM_FT float
#undef UINT_MAX
#define UINT_MAX 0xffffffff
#define U32MAX 0xffffffff
#define U16MAX 0xffff
#define U8MAX 0xff

_TKMNamespaceBegin

class ObjectBase;

typedef float ft;
typedef int wstrHash;
typedef int strHash;
//定点小数，百分比。
typedef short pct;

#define LogPrintf(format, level, ...) printf(format, ##__VA_ARGS__)

//debug
enum PrintDataType_e : unsigned char {
    PDT_Debug,
    PDT_Tree,
    PDT_MAX,
};

//节点类型枚举
enum NodeType_e : unsigned short {
    NT_ObjectBase,              //base

    NT_NodeTypeSetting,         //ObjectBase:NodeTypeSetting
    NT_AttrSetting,             //ObjectBase:AttrSetting
    NT_Unit,

    NT_MAX,
};

#define NT_Block NT_SimpleComponent
#define NT_Panel NT_ContainerComponent

//属性类型枚举
enum AttrType_e : unsigned short {
    AT_NULL,
    AT_MAX,
};

enum AttrDataType_e : unsigned char {
    ADT_B2,
    ADT_S32,
    ADT_U32,
    ADT_float,
    ADT_S64,
    ADT_HU32,
    ADT_HS64,
    ADT_STR,
    ADT_WSTR,
    ADT_MAX
};

enum AttrSubType_e : unsigned char {
    WordRowRegisterWNO(AttrSubType, AST_, halfNormal)
    WordRowRegisterWNO(AttrSubType, AST_, normal)
    WordRowRegisterWNO(AttrSubType, AST_, allBool)
    WordRowRegisterWNO(AttrSubType, AST_, MAX)
};

#pragma region "CSV"
enum CsvOptType_e : unsigned char {
    CSV_TYPE_ASSIGN,
    CSV_TYPE_COPY,
    CSV_TYPE_CACHE,
    CSV_TYPE_DELETE,
    CSV_TYPE_HIDE,
    CSV_TYPE_ITEM,
    CSV_TYPE_APPEND,
    CSV_TYPE_CACHE_2,
    CSV_TYPE_ASSIGN_NODELETE,
    CSV_TYPE_MAX
};

#define CAT_MULTIPLEX_VALUE ((1 << CAT_Value1) | (1 << CAT_Value2))
#pragma endregion

template<class NodeType>
struct Point {
public:
    NodeType m_x;
    NodeType m_y;
    inline Point<NodeType> operator+(const Point<NodeType>& other) const {
        return { m_x + other.m_x, m_y + other.m_y };
    }
    inline Point<NodeType> operator-(const Point<NodeType>& other) const {
        return { m_x - other.m_x, m_y - other.m_y };
    }
    inline Point<NodeType> operator+=(const Point<NodeType>& other) {
        m_x += other.m_x;
        m_y += other.m_y;
        return *this;
    }
    inline Point<NodeType> operator-=(const Point<NodeType>& other) {
        m_x -= other.m_x;
        m_y -= other.m_y;
        return *this;
    }
};

template <class DataType> class Rect {
public:
    DataType m_x = 0.f;
    DataType m_y = 0.f;
    DataType m_w = 0.f;
    DataType m_h = 0.f;
    inline Rect() {

    }
    inline Rect(DataType x, DataType y, DataType w = 0.f, DataType h = 0.f) : m_x(x), m_y(y), m_w(w), m_h(h) {

    }
    inline bool operator==(const Rect& other) const {
        if (m_w == other.m_w && m_h == other.m_h && m_x == other.m_x && m_y == other.m_y) {
            return true;
        } else {
            return false;
        }
    }
    inline bool operator!=(const Rect& other) const {
        if (m_w != other.m_w || m_h != other.m_h || m_x != other.m_x || m_y != other.m_y) {
            return true;
        } else {
            return false;
        }
    }
};

_TKMNamespaceEnd
