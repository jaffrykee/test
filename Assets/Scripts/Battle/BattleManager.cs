using System;
using System.Collections.Generic;
using TkmGame.Core;

namespace TkmGame.Gtr.Battle {
    /// <summary>
    /// 战斗控制类。
    /// </summary>
    public class BattleManager : Repertory {
        static private BattleManager s_instance = new BattleManager();
        static public BattleManager instance() {
            return s_instance;
        }

        private BattleManager() : base(
            new Dictionary<Type, Object>
            {
            {typeof(League), new List<League>()},
            {typeof(Army), new List<Army>()},
            {typeof(Unit), new List<Unit>()},
            {typeof(Turn), new List<Turn>()},
            {typeof(Operation), new List<Operation>()},
            },
            new Dictionary<string, Goods>()) {
        }
        public void resetData(Stage data) {

        }

        public Stage m_curStage;
        public CellMap m_curCellMap;
    }
}
