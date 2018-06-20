using TkmGame.Core;
using System.Collections.Generic;

namespace TkmGame.Gtr.Battle {
    /// <summary>
    /// 代表了一个单位，可以是随从、放置物、陷阱、可破坏地貌等等。必须属于一个部队。
    /// </summary>
    public class Unit : Goods {
        public Army m_army;
        public string m_cardName;
        public string m_text;
        //所属
        public int m_groupId;

        public UnitProperty m_basicProperty;

        public List<EffectFunc>[] m_arrEffect = new List<EffectFunc>[(int)EffectEvent.MAX];
        public List<UnitProperty>[] m_arrBuff;

        public Unit(string name, Card card) : base(BattleManager.instance(), name) {
        }
        public void onEffectEvent(EffectEvent e, Unit unit) {
            var fl = m_arrEffect[(int)e];
            if (fl != null) {
                foreach (var ef in fl) {
                    ef(this, unit);
                }
            }
        }
    }
}