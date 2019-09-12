#pragma once
#include <unordered_map>

template <class KeyType, class MappedType, class Hash = std::hash<KeyType>, class Pred = std::equal_to<KeyType> >
class LinkedHashMap {
public:
    typedef std::pair<const KeyType, MappedType> EntryType;
    typedef typename std::list<EntryType>::iterator iterator;
    typedef typename std::list<EntryType>::const_iterator const_iterator;

private:
    typedef std::unordered_map<KeyType, iterator, Hash, Pred> HashMap;
    typedef typename HashMap::iterator _map_itr;
    typedef typename HashMap::const_iterator _map_citr;
    size_t _size;
    std::list<EntryType> _values;
    HashMap _map_key2entry;

public:
    std::pair<iterator, bool> insert(const EntryType& new_entry) {
        _map_itr pIt = _map_key2entry.find(new_entry.first);
        if (pIt == _map_key2entry.end()) {
            _values.push_back(new_entry);
            _map_key2entry.insert(std::pair<KeyType, iterator>(new_entry.first, --_values.end()));
            ++_size;
        } else {
            _values.erase(pIt->second);
            _values.push_back(new_entry);
            pIt->second = --_values.end();
        }
        return std::pair<iterator, bool>(--_values.end(), true);
    }

    iterator insert(const_iterator it, const EntryType& new_entry) {
        _map_itr pIt = _map_key2entry.find(new_entry.first);
        if (pIt != _map_key2entry.end()) {
            _values.erase(pIt->second);
            --_size;
        }
        iterator _it = _values.insert(it, new_entry);
        _map_key2entry.insert(std::pair<KeyType, iterator>(new_entry.first, _it));
        ++_size;
        return _it;
    }

    inline iterator begin() {
        return _values.begin();
    }

    inline const_iterator begin() const {
        return _values.begin();
    }

    inline iterator end() {
        return _values.end();
    }

    inline const_iterator end() const {
        return _values.end();
    }

    iterator find(const KeyType& key) {
        if (_size > 0) {
            _map_itr it = _map_key2entry.find(key);
            if (it == _map_key2entry.end()) {
                return _values.end();
            }
            iterator find_it = it->second;
            return find_it;
        }
        return _values.end();
    }

    const_iterator find(const KeyType& key) const {
        if (_size > 0) {
            _map_citr it = _map_key2entry.find(key);
            if (it == _map_key2entry.end()) {
                return _values.end();
            }
            const_iterator find_it = it->second;
            return find_it;
        }
        return _values.end();
    }

    void erase(iterator pos) {
        assert(pos != _values.end());
        assert(_size != 0);
        EntryType v = *pos;
        _values.erase(pos);
        _map_key2entry.erase(v.first);
        --_size;
    }

    void erase(const KeyType& key) {
        iterator it = find(key);
        if (it != _values.end()) {
            erase(it);
        }
    }

    void clear() {
        _values.clear();
        _map_key2entry.clear();
        _size = 0;
    }

    inline size_t size() {
        return _size;
    }

    inline bool empty() const {
        return _values.empty();
    }

};
