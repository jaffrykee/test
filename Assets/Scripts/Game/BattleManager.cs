using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

public class BattleManager : Repertory
{
    static private BattleManager s_instance = new BattleManager();
    static public BattleManager instance()
    {
        return s_instance;
    }

    private BattleManager() : base()
    {
        m_data = new Dictionary<Type, Object>
        {
            {typeof(League), new List<League>()},
            {typeof(Army), new List<Army>()},
            {typeof(Unit), new List<Unit>()},
            {typeof(Turn), new List<Turn>()},
            {typeof(Operation), new List<Operation>()},
        };
    }
}
