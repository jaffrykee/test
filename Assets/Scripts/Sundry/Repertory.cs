using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;

public class Repertory {
    protected Dictionary<Type, object> m_data;
    private Dictionary<string, Goods> m_dictionary;
    public List<T> getList<T>() {
        object lst;
        if (m_data.TryGetValue(typeof(T), out lst) == true && lst != null) {
            return lst as List<T>;
        }
        return null;
    }
    public int indexOf<T>(T item) {
        var lst = getList<T>();
        if (lst != null) {
            return lst.IndexOf(item);
        } else {
            return -1;
        }
    }
    public void addItem<T>(T item) {
        var lst = getList<T>();
        if (lst != null) {
            lst.Add(item);
        }
        var goods = item as Goods;
        if (goods != null) {
            if (goods.m_name == null || goods.m_name.Length <= 0) {
                Debug.LogWarning("Repertory.cs: Goods's name is Null.");
            } else {
                if (m_dictionary.ContainsKey(goods.m_name)) {
                    Debug.LogWarning("Repertory.cs: Goods's name is existed in the dictionary.");
                } else {
                    m_dictionary.Add(goods.m_name, goods);
                }
            }
        }
    }
    public void deleteItem<T>(T item) {
        var lst = getList<T>();
        if (lst != null) {
            lst.Remove(item);
        }
    }
}
