#ifdef _WIN32
#include <io.h>
#else
#include <sys/uio.h>
#endif
#include "DataManager.h"
#include "TKMLib.h"
#include "AttrSetting.h"
#include "NodeTypeSetting.h"
#include "StringManager.h"

static string sFileName;
static string sFileData;
string DataManager::s_className = "DataManager";
DataManager* DataManager::s_pInstance = nullptr;

void DataManager::registerReflection(int id) {
    //registerFunc(id, "loadUI", bolo_ui_loadUI, "(UIScene*)loadUI(string name)");
}

const std::bitset<NT_MAX>& DataManager::getNodeInheritData(NodeType_e nt) {
    return m_arrNodeTypeSetting[nt]->m_inheritData;
}

ObjectBase* DataManager::getInitNode(NodeType_e nodeType) {
    return m_arrNodeTypeSetting[nodeType]->m_pInitNode;
}

void DataManager::initialize() {
    auto pSelf = getInstance();
    for (auto& pAs : pSelf->m_arrAttrSetting) {
        if (pAs != nullptr) {
            pSelf->m_mapAttrSetting.insert(pair<wstrHash, AttrSetting*>(hash<wstring>()(pAs->m_attrName), pAs));
        }
    }
}

void DataManager::destroy() {
    auto pSelf = getInstance();
    pSelf->clearCache();
}

void DataManager::clearCache() {
}

void DataManager::getFiles(vector<string>& arrFiles, const string& path, const string& fix, bool isIterative) {
#ifdef _WIN32
    //文件句柄
    size_t hFile = 0;
    //文件信息
    struct _finddata_t fileinfo;
    string p = path;
    if ((hFile = _findfirst(p.append(fix).c_str(), &fileinfo)) != -1) {
        do {
            //如果是目录,迭代之
            //如果不是,加入列表
            if ((fileinfo.attrib & _A_SUBDIR)) {
                if (isIterative && strcmp(fileinfo.name, ".") != 0 && strcmp(fileinfo.name, "..") != 0) {
                    getFiles(arrFiles, p.append("\\").append(fileinfo.name), fix, isIterative);
                }
            } else {
                arrFiles.push_back(fileinfo.name);
            }
        } while (_findnext(hFile, &fileinfo) == 0);
        _findclose(hFile);
    }
#endif
}

void DataManager::initTKMFunc() {
    //DataManager::loadUISceneFromTemplate("a.sui");
}
