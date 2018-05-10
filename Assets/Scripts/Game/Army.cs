using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Army
{
    public string m_name;
    public int m_index
    {
        get
        {
            return BattleManager.s_instance.indexOf(this);
        }
    }
}
