using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 部队，代表了一个控制者，或者一个AI。
/// </summary>
public class Stage : Goods {
    public Stage(string name) : base(BattleManager.instance(), name) {
    }
}
