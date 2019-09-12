#include "ResPoolManager.h"

ResPool<vector<EventNodeBase*>> RPM::s_rpEventNode = ResPool<vector<EventNodeBase*>>();
ResPool<vector<UIComponent*>> RPM::s_rpIEventComponent = ResPool<vector<UIComponent*>>();
ResPool<vector<UIComponent*>> RPM::s_rpIDrawComponent = ResPool<vector<UIComponent*>>();
//ResPool<std::unordered_map<tkm::strHash, BoloVar>> tkm::ResPoolManager::s_rpMapUserAttr = ResPool<std::unordered_map<tkm::strHash, BoloVar>>();
tkm::ResPool<std::vector<std::wstring>> tkm::ResPoolManager::s_rpArrStringW;

tkm::ResPool<std::string> tkm::ResPoolManager::s_rpString;
tkm::ResPool<std::wstring> tkm::ResPoolManager::s_rpStringW;
