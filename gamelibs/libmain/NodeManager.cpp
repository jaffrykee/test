#include "ObjectBase.h"
#include "NodeManager.h"
#include "StringManager.h"
#include <stdio.h>

string NodeManager::s_className = "NodeManager";
NodeManager* NodeManager::s_pInstance = nullptr;

void NodeManager::initialize() {
    //NT_(.*),[/ A-Za-z:]*
    //$1::initialize();
    ObjectBase::initialize();

    NodeManager::getInstance()->m_isReady = true;
}

void NodeManager::destroy() {
    getInstance()->m_isReady = false;

    //initialize
    //destroy
    ObjectBase::destroy();

    safe_delete(s_pInstance);
}