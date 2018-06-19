using TkmGame.Core;
using System.Collections.Generic;

namespace TkmGame.Gtr.Battle {
    /// <summary>
    /// 代表了一个单位，可以是随从、放置物、陷阱、可破坏地貌等等。必须属于一个部队。
    /// </summary>
    public class Unit : Goods {
        public Army m_army;
        public Card m_card;
        public string m_text;
        //所属
        public int m_groupId;

        public int m_basicAttack;
        public int m_basicDefense;
        public int m_basicSupply;

        public int m_basicAttackRange = 1;
        public int m_basicSupplyRange = 1;

        public List<EffectFunc>[] m_arrEffect = new List<EffectFunc>[(int)EffectEvent.MAX];

        public Unit(string name, Army army) : base(BattleManager.instance(), name) {
            m_army = army;
        }
        public void onEffectEvent(EffectEvent e, Unit unit) {
            var fl = m_arrEffect[(int)e];
            if (fl != null) {
                foreach (var ef in fl) {
                    ef(this, unit);
                }
            }
        }

        public void onBorn() {

        }
        public void onEnter() {

        }
        public void onDead() {

        }
        public void onExit() {

        }
        public void onUnitEnter(Unit unit) {

        }
        public void onOtherUnitDead(Unit unit, bool isNormal) {

        }
        public void onOtherUnitInjured(Unit unit) {

        }
    }
}