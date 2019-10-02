#pragma once
#include "tkmBasic.h"

using namespace std;
using namespace tkm;

_TKMNamespaceBegin

//一个静态类
class StringManager {
#pragma region "静态成员"
public:
    static string s_className;
    static StringManager* s_pInstance;
#pragma endregion

#pragma region "静态方法"
public:
    inline static StringManager* getInstance() {
        if (s_pInstance == nullptr) {
            s_pInstance = new StringManager();
        }
        return s_pInstance;
    }
    static void initialize();
    static void destroy();
    static void registerReflection(int id);

    template<typename TypeIn, typename TypeOut, size_t Count>
    inline static void RegisterWordRow(TypeIn(&arr)[Count], unordered_map<TypeIn, int>& map, TypeOut outStr) {
        int i = 0;
        outStr.clear();
        for (const auto& rowName : arr) {
            map.insert(rowName, i);
            i++;
            outStr += TypeOut(rowName.c_str());
            outStr += '%';
        }
        outStr.pop_back();
    }
    template<typename TypeIn, size_t Count>
    inline static void RegisterWordRow(TypeIn(&arr)[Count], unordered_map<TypeIn, int>& map) {
        int i = 0;
        for (const auto& rowName : arr) {
            map.insert(rowName, i);
            i++;
        }
    }
#pragma endregion

#pragma region "成员"
public:
    const string mc_strNullDef = "";
    const wstring mc_wstrNullDef = L"";
    const string mc_strTrueDef = "true";
    const wstring mc_wstrTrueDef = L"true";
    const string mc_strFalseDef = "false";
    const wstring mc_wstrFalseDef = L"false";
    const string mc_strDefault = "default";
    const wstring mc_wstrDefault = L"default";

    const wstring mc_wstrTKM = L"TKM";

    const string mc_strUiDataPackage = "ui/";
    const string mc_strScriptPath = "script/";

    unordered_map<int, wstring> m_mapAttrSetting;
    unordered_set<string> m_hsThemeName;
    vector<string> m_arrTmpSplit;
    vector<wstring> m_arrTmpWSplit;

    const unsigned short mc_wcOpenBrace = '{';
    const unsigned short mc_wcCloseBrace = '}';
    const unsigned short mc_wcOpenBracket = '[';
    const unsigned short mc_wcCloseBracket = ']';
    const unsigned short mc_wcOpenParentheses = '(';
    const unsigned short mc_wcCloseParentheses = ')';
    const unsigned short mc_wcPeriod = '.';
    const unsigned short mc_wcTextFlowEnter = '`';
    const unsigned short mc_wcTextFlowScript = '@';
    const unsigned short mc_wcSkinSep = ';';
    const unsigned short mc_wcPasswordChar = '*';

    const string mc_strEmpty = "";
    const wstring mc_wstrEmpty = L"";
    const string mc_strUITextureFileSuffix = ".xml";

    string m_tmpString;
    wstring m_tmpWString;
#pragma endregion

#pragma region "方法"
public:
    StringManager();
public:
    inline static string& str2str(const wstring& ws, string& result) {
        _bstr_t t = ws.c_str();
        char* pchar = (char*)t;
        result = pchar;
        return result;
    }

    inline static wstring& str2str(const string& s, wstring& result) {
        _bstr_t t = s.c_str();
        wchar_t* pwchar = (wchar_t*)t;
        result = pwchar;
        return result;
    }
    inline static void getMemStringFromByteNum(string& memString, int byteNum) {
        if (byteNum > 1024 * 1024) {
            memString = to_string(byteNum / (1024 * 1024)) + " MB";
        } else if (byteNum > 1024) {
            memString = to_string(byteNum / 1024) + " KB";
        } else {
            memString = to_string(byteNum);
        }
    }
    inline static void wstringToString(const wstring& src, string& dst) {
        dst.clear();
        str2str(src, dst);
    }
    inline static void stringToWstring(const string& src, wstring& dst) {
        dst.clear();
        str2str(src, dst);
    }
    inline static bool getBoolValue(const wstring& wstr) {
        return (wstr.front() == 't') ? true : false;
    }
    static void appendStrings(string& dstStr, const string& append, int count);
#pragma endregion
};

_TKMNamespaceEnd
