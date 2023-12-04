using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ItemDropInfo
{
    public ItemData item;
    public float dropRate;
}

[Serializable]
public class EnemyPhaseData
{
    public float health;
    public float attack;
    public int level;
    public float defend;
    public int expGiven;
    public List<ItemDropInfo> itemDrops;

    public EnemyPhaseData(float health, float attack, int level, float defend, int expGiven, List<ItemDropInfo> itemDrops)
    {
        this.health = health;
        this.attack = attack;
        this.level = level;
        this.defend = defend;
        this.expGiven = expGiven;
        this.itemDrops = itemDrops;
    }
}

[Serializable]
public class EnemyData
{
    public List<EnemyPhaseData> ListPhaseData { get; set; }

    public EnemyData(List<EnemyPhaseData> list)
    {
        this.ListPhaseData = list;
    }
}

public class EnemyStat : MonoBehaviour
{
    public List<EnemyPhaseData> listPhase;

    public float Health
    {
        get; set;
    }

    public float Attack
    {
        get; set;
    }

    public int Level
    {
        get; set;
    }

    public float Defend
    {
        get; set;
    }

    public int ExpGiven
    {
        get; private set;
    }

    public int CurrentPhase
    {
        get; set;
    }

    private void Start()
    {
        CurrentPhase = 0;
        Health = listPhase[CurrentPhase].health;
        Attack = listPhase[CurrentPhase].attack;
        Level = listPhase[CurrentPhase].level;
        Defend = listPhase[CurrentPhase].defend;
        ExpGiven = listPhase[CurrentPhase].expGiven;
    }
}
