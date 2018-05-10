using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BattleManager
{
    static public readonly BattleManager s_instance = new BattleManager();
    static public BattleManager instance()
    {
        return s_instance;
    }
    private List<Army> m_armys;
    private List<League> m_leagues;
    private List<Operation> m_operations;

    public int indexOf(Army a)
    {
        return m_armys.IndexOf(a);
    }
    public int indexOf(Operation op)
    {
        return m_operations.IndexOf(op);
    }
}
