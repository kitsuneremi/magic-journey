using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public float health;
    public float mana;
    public int exp;
    public float defend;
    public float attack;

    public PlayerData(float health, float mana, int exp, float defend, float attack)
    {
        this.health = health;
        this.mana = mana;
        this.exp = exp;
        this.defend = defend;
        this.attack = attack;
    }
}

public class PlayerStat : MonoBehaviour
{
    public PlayerData playerData;

    public float Health
    {
        get; set;
    }

    public float Mana
    {
        get; set;
    }

    public int Exp
    {
        get; set;
    }

    public float Defend
    {
        get; set;
    }

    public float Attack
    {
        get; set;
    }

    private void Start()
    {
        Health = playerData.health;
        Mana = playerData.mana;
        Exp = playerData.exp;
        Defend = playerData.defend;
        Attack = playerData.attack;
    }
}