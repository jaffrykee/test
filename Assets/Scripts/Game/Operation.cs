using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Operation {
    public int m_index {
        get {
            return BattleManager.s_instance.indexOf(this);
        }
    }
}
