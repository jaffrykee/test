using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Turn : Goods {
    public Army m_army;
    public Turn(string name, Army army) : base(BattleManager.instance(), name) {
        m_army = army;
    }
}
