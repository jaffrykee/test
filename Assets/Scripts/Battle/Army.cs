using System.Linq;
using UnityEngine;
using TkmGame.Core;

namespace TkmGame.Gtr.Battle {
    /// <summary>
    /// 部队，代表了一个控制者，或者一个AI。
    /// </summary>
    public class Army : Goods {
        public enum ArmyType {
            Player = 0,
            AI,
        }
        public League m_league;
        public ArmyType m_type = ArmyType.Player;

        public Army(string name, League league, ArmyType type = ArmyType.Player) : base(BattleManager.instance(), name) {
            m_league = league;
            m_type = type;
        }
        public Army(string name, int leagueIndex, ArmyType type = ArmyType.Player) : base(BattleManager.instance(), name) {
            m_type = type;
            var bm = BattleManager.instance();
            var le = bm.getList<League>();
            if (leagueIndex < le.Count) {
                m_league = le[leagueIndex];
            } else {
                Debug.LogWarning("The league index is not exist.It will be created.");
                League curLg = null;
                for (int i = le.Count; i <= leagueIndex; i++) {
                    curLg = new League("League" + leagueIndex.ToString());
                }
                m_league = curLg;
            }
        }
        public Army(string name, string leagueName, ArmyType type = ArmyType.Player) : base(BattleManager.instance(), name) {
            m_type = type;
            var bm = BattleManager.instance();
            Goods gd;
            if (bm.m_dictionary.TryGetValue(leagueName, out gd) == true && gd is League) {
                m_league = gd as League;
            } else {
                m_league = new League(leagueName);
            }
        }
        public Army(ArmyData data) : base(BattleManager.instance(), data.name) {
            m_type = (ArmyType)data.type;
            var bm = BattleManager.instance();
            Goods gd;
            if (bm.m_dictionary.TryGetValue(data.league, out gd) == true && gd is League) {
                m_league = gd as League;
            } else {
                m_league = new League(data.league);
            }
        }
    }
}