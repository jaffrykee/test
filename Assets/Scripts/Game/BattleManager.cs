using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

public class BattleManager
{
    static public readonly BattleManager s_instance = new BattleManager();
    static public BattleManager instance()
    {
        return s_instance;
    }

    private Dictionary<Type, Object> m_data = new Dictionary<Type, Object>
    {
        {typeof(Army), new List<Army>()},
        {typeof(League), new List<League>()},
        {typeof(Operation), new List<Operation>()},
    };

    #region 容器
    public List<T> getList<T>()
    {
        Object lst;
        if (m_data.TryGetValue(typeof(T), out lst) == true && lst != null)
        {
            return lst as List<T>;
        }
        return null;
    }
    public int indexOf<T>(T item)
    {
        var lst = getList<T>();
        if (lst != null)
        {
            return lst.IndexOf(item);
        }
        else
        {
            return -1;
        }
    }
    public void addItem<T>(T item)
    {
        var lst = getList<T>();
        if (lst != null)
        {
            lst.Add(item);
        }
    }
    public void deleteItem<T>(T item)
    {
        var lst = getList<T>();
        if (lst != null)
        {
            lst.Remove(item);
        }
    }
    #endregion
}
