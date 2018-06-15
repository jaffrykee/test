using TkmGame.Core;

namespace TkmGame.Gtr.Battle {
    /// <summary>
    /// 回合。
    /// </summary>
    public class Turn : Goods {
        public Army m_army;
        public Turn(string name, Army army) : base(BattleManager.instance(), name) {
            m_army = army;
        }
    }
}
