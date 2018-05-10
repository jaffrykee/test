using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public int m_index
    {
        get
        {
            return BattleManager.s_instance.indexOf(this);
        }
    }

    public string m_name;
    public string m_text;
    //所属
    public int m_groupId;

    public int m_basicAttack;
    public int m_basicDefense;
    public int m_basicSupply;

    public int m_basicAttackRange = 1;
    public int m_basicSupplyRange = 1;

}