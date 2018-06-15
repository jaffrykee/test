using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;

namespace TkmGame.Core {
    /// <summary>
    /// 仓库类，用于存放货物(Goods)。
    /// </summary>
    public class Repertory {
        /// <summary>
        /// 按类型存放的货物队列词典。
        /// </summary>
        protected Dictionary<Type, object> m_data;
        /// <summary>
        /// 以货物名字为索引的词典。（数据）
        /// </summary>
        private Dictionary<string, Goods> mt_dictionary;
        /// <summary>
        /// 以货物名字为索引的词典。（对外接口）
        /// </summary>
        public Dictionary<string, Goods> m_dictionary {
            get {
                return mt_dictionary;
            }
        }
        /// <summary>
        /// 仓库类，用于存放货物(Goods)。
        /// </summary>
        /// <param name="data">按类型存放的货物队列词典。</param>
        /// <param name="dictionary">以货物名字为索引的词典。</param>
        public Repertory(Dictionary<Type, object> data, Dictionary<string, Goods> dictionary) {
            m_data = data;
            mt_dictionary = dictionary;
        }
        /// <summary>
        /// 得到所有T类型的货物的List。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> getList<T>() {
            object lst;
            if (m_data.TryGetValue(typeof(T), out lst) == true && lst != null) {
                return lst as List<T>;
            }
            return null;
        }
        /// <summary>
        /// 查看货物在仓库中同类型的所有货物中的索引。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public int indexOf<T>(T item) {
            var lst = getList<T>();
            if (lst != null) {
                return lst.IndexOf(item);
            } else {
                return -1;
            }
        }
        /// <summary>
        /// 将item登记在仓库的词典中，item新创建或被改名的时候会调用。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">要被登记的对象</param>
        public void record<T>(T item) {
            var goods = item as Goods;
            if (goods != null) {
                if (goods.m_name == null || goods.m_name.Length <= 0) {
                    Debug.LogWarning("Repertory.cs: Goods's name is Null.");
                } else {
                    if (m_dictionary.ContainsKey(goods.m_name)) {
                        Debug.LogWarning("Repertory.cs: Goods's name is existed in the dictionary.The old one will lost in the dictionary.");
                        m_dictionary[goods.m_name] = goods;
                    } else {
                        m_dictionary.Add(goods.m_name, goods);
                    }
                }
            } else {
                Debug.LogWarning("Repertory.cs: This item (Type: " + item.GetType().ToString() + ") is not a Goods object, it can't put into dictionary.");
            }
        }
        /// <summary>
        /// 将货物加入到仓库中。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void addItem<T>(T item) {
            var lst = getList<T>();
            if (lst != null) {
                lst.Add(item);
            }
            record(item);
        }
        /// <summary>
        /// 将item从字典中抹除。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
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
        /// <summary>
        /// 从仓库中移除货物。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void deleteItem<T>(T item) {
            var lst = getList<T>();
            if (lst != null) {
                lst.Remove(item);
            }
            abrase(item);
        }
    }
}
