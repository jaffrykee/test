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

_TKMNamespaceEnd
