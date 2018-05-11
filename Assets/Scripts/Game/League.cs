using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class League : Goods {
    public bool isOwner(Army a) {
        return a.m_league == this;
    }
    public List<Army> m_armys {
        get {
            return BattleManager.instance().getList<Army>().FindAll(isOwner);
        }
    }
}
