using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 卡片。
/// </summary>
public class Card : Goods {
    public enum CardType {
        
    }
    public Army m_army;
    public Card(string name, Army army) : base(BattleManager.instance(), name) {
        m_army = army;
    }
}
