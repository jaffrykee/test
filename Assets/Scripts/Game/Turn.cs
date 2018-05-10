using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Turn
{
    public int m_index
    {
        get
        {
            return BattleManager.s_instance.indexOf(this);
        }
    }
}
