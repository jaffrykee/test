using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Turn : Goods {

    public Turn(string name) : base(BattleManager.instance(), name) {
    }
}
