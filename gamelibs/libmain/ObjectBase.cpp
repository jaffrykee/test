#include "ObjectBase.h"

#include "NodeManager.h"
#include "StringManager.h"

NODETYPE_COMMON_PART_DEFINITION_BEGIN(ObjectBase, 0, 0);
#pragma region "ÊôÐÔ×¢²á"
//NODEBASE_ATTR_REGISTER(L"ssueId", SsueId, ObjectBase, HS64);
#pragma endregion
NODETYPE_COMMON_PART_DEFINITION_END

ObjectBase::ObjectBase() {
    createSelf();
}

void ObjectBase::createSelf() {

}

void ObjectBase::disposeSelf() {
}
