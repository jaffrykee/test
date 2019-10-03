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

void NodeManager::showObjectCount() {
    int tmpSize, tmpCount, tmpMem, tmpRpCount, tmpRpMem, tmpTrueCount, tmpTrueMem;
    string tmpOutMem, tmpOutRpMem, tmpOutTrueMem;
    int totalCount = 0, totalMem = 0, totalRpCount = 0, totalRpMem = 0, totalTrueCount = 0, totalTrueMem = 0;
    printf("=============================================================================");
    printf("===============================showObjectCount===============================");
    printf("=============================================================================");
    printf("size\tcount\tmem\trpCount\trpMem\ttCount\ttMem\tname\t");
    for (auto& pairTestData : m_mapClassTestData) {
        tmpSize = (*pairTestData.second.m_pFuncGetSizeObject)();
        tmpCount = (*pairTestData.second.m_pFuncGetCountObject)();
        tmpMem = tmpCount * tmpSize;
        StringManager::getMemStringFromByteNum(tmpOutMem, tmpMem);
        tmpRpCount = (*pairTestData.second.m_pFuncGetResPoolCount)();
        tmpRpMem = tmpRpCount * tmpSize;
        StringManager::getMemStringFromByteNum(tmpOutRpMem, tmpRpMem);
        tmpTrueCount = tmpCount > tmpRpCount ? tmpCount : tmpRpCount;
        tmpTrueMem = tmpMem > tmpRpMem ? tmpMem : tmpRpMem;
        StringManager::getMemStringFromByteNum(tmpOutTrueMem, tmpTrueMem);
        if (tmpCount != 0 || tmpRpCount != 0) {
            //printlog("%8d\t%s\n", tmpCount, pairTestData.first.c_str());
            printf("%d\t%d\t%s\t%d\t%s\t%d\t%s\t%s", tmpSize, tmpCount, tmpOutMem.c_str(),
                tmpRpCount, tmpOutRpMem.c_str(), tmpTrueCount, tmpOutTrueMem.c_str(), pairTestData.first.c_str());
            totalCount += tmpCount;
            totalMem += tmpMem;
            totalRpCount += tmpRpCount;
            totalRpMem += tmpRpMem;
            totalTrueCount += tmpTrueCount;
            totalTrueMem += tmpTrueMem;
        }
    }
    StringManager::getMemStringFromByteNum(tmpOutMem, totalMem);
    StringManager::getMemStringFromByteNum(tmpOutRpMem, totalRpMem);
    StringManager::getMemStringFromByteNum(tmpOutTrueMem, totalTrueMem);
    printf("\t%d\t%s\t%d\t%s\t%d\t%s\t<Total>", totalCount, tmpOutMem.c_str(), totalRpCount, tmpOutRpMem.c_str(),
        totalTrueCount, tmpOutTrueMem.c_str());
}
