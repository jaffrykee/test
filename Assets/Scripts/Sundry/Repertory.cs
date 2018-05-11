using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;

public class Repertory {
    protected Dictionary<Type, object> m_data;
    private Dictionary<string, Goods> mt_dictionary;
    protected Dictionary<string, Goods> m_dictionary {
        get {
            return mt_dictionary;
        }
    }

    public Repertory(Dictionary<Type, object> data, Dictionary<string, Goods> dictionary) {
        m_data = data;
        mt_dictionary = dictionary;
    }

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
    public void record<T>(T item) {
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
        } else {
            Debug.LogWarning("Repertory.cs: This item (Type: " + item.GetType().ToString() + ") is not a Goods object, it can't put into dictionary.");
        }
    }
    public void addItem<T>(T item) {
        var lst = getList<T>();
        if (lst != null) {
            lst.Add(item);
        }
        record(item);
    }
    public void abrase<T>(T item) {
        var goods = item as Goods;
        if (goods != null) {
            var curNameGoods = m_dictionary[goods.m_name];
            if (curNameGoods == goods) {
                m_dictionary.Remove(goods.m_name);
            } else {
                Debug.LogWarning("Repertory.cs: The name owner that in the dictionary, is not the item.");
            }
        } else {
            Debug.LogWarning("Repertory.cs: This item (Type: " + item.GetType().ToString() + ") is not a Goods object");
        }
    }
    public void deleteItem<T>(T item) {
        var lst = getList<T>();
        if (lst != null) {
            lst.Remove(item);
        }
        abrase(item);
    }
}
