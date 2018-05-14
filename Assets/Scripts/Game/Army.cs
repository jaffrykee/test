﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 部队，代表了一个控制者，或者一个AI。
/// </summary>
public class Army : Goods {
    public League m_league;
    public Army(string name, League league) : base(BattleManager.instance(), name) {
        m_league = league;
    }
}
