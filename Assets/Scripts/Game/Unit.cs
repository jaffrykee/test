using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 代表了一个单位，可以是随从、放置物、陷阱、可破坏地貌等等。必须属于一个部队。
/// </summary>
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