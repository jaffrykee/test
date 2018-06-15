using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TkmGame.Core {
    /// <summary>
    /// 货物类，用于存放需要全局（仓库）字符串索引和同类别队列索引的对象。
    /// </summary>
    public class Goods {
        /// <summary>
        /// 所属的仓库
        /// </summary>
        private Repertory mt_rep;
        public Repertory m_rep {
            get {
                return mt_rep;
            }
        }
        /// <summary>
        /// 在仓库词典中的索引。
        /// </summary>
        private string mt_name;
        public string m_name {
            get {
                return mt_name;
            }
            set {
                m_rep.abrase(this);
                mt_name = value;
                m_rep.record(this);
            }
        }
        /// <summary>
        /// 在仓库对应类型序列中的索引。
        /// </summary>
        public int m_index {
            get {
                return m_rep.indexOf(this);
            }
        }
        /// <summary>
        /// 货物类，用于存放需要全局(Repertory)字符串索引和同类别队列索引的对象。
        /// </summary>
        /// <param name="rep">所属的仓库。</param>
        /// <param name="name">在仓库词典中的索引。</param>
        public Goods(Repertory rep, string name) {
            mt_name = name;
            mt_rep = rep;
            rep.addItem(this);
        }
    }
}
