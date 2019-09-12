#include "StringManager.h"
#include "NodeManager.h"

string StringManager::s_className = "StringManager";
StringManager* StringManager::s_pInstance = nullptr;

void StringManager::initialize() {
    //ET_([a-zA-Z]*);
    //"$1";
}

void StringManager::destroy() {
    safe_delete(s_pInstance);
}

void StringManager::registerReflection(int id) {

}

void tkm::StringManager::appendStrings(string& dstStr, const string& append, int count) {
    for (int i = 0; i < count; i++) {
        dstStr.append(append);
    }
}

tkm::StringManager::StringManager() {
}
