using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Goods {
    public Army m_army;
    public string m_text;
    //所属
    public int m_groupId;

    public int m_basicAttack;
    public int m_basicDefense;
    public int m_basicSupply;

    public int m_basicAttackRange = 1;
    public int m_basicSupplyRange = 1;

    public Unit(string name, Army army) : base(BattleManager.instance(), name) {
        m_army = army;
    }
}