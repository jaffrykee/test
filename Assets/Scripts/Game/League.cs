using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 联盟，代表了共用了补给的n个部队(n>0)。
/// </summary>
public class League : Goods {
    public League(string name) : base(BattleManager.instance(), name) {

    }
    public List<Army> m_armies {
        get {
            return BattleManager.instance().getList<Army>().FindAll(isOwner);
        }
    }
    public bool isOwner(Army a) {
        return a.m_league == this;
    }
}
