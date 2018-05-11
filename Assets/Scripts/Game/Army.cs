using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Army : Goods {
    public League m_league;
    public int m_index {
        get {
            return BattleManager.instance().indexOf(this);
        }
    }
    public Army(string name, League league) : base(BattleManager.instance(), name) {
        m_league = league;
    }
}
