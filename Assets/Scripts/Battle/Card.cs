using TkmGame.Core;
using System.Collections;
using System.Collections.Generic;

namespace TkmGame.Gtr.Battle {
    /// <summary>
    /// 卡片。
    /// </summary>
    public class Card : Goods {
        public enum CardType {

        }
        public Army m_army;
        public List<EffectFunc> m_beList;

        public Card(string name, Army army) : base(BattleManager.instance(), name) {
            m_army = army;
        }
    }
}
