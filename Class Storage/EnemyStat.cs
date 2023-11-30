using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemDropInfo
{
    public ItemData item;
    public float dropRate;
}

[Serializable]
public class EnemyData
{
    public float health;
    public float attack;
    public int level;
    public float defend;
    public int expGiven;
    public List<ItemDropInfo> itemDrops;

    public EnemyData(float health, float attack, int level, float defend, int expGiven, List<ItemDropInfo> itemDrops)
    {
        this.health = health;
        this.attack = attack;
        this.level = level;
        this.defend = defend;
        this.expGiven = expGiven;
        this.itemDrops = itemDrops;
    }
}

public class EnemyStat : MonoBehaviour
{
    public EnemyData enemyData;

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
    public List<ItemDropInfo> ItemDrops { get { return enemyData.itemDrops; } }

    private void Start()
    {
        Health = enemyData.health;
        Attack = enemyData.attack;
        Level = enemyData.level;
        Defend = enemyData.defend;
        ExpGiven = enemyData.expGiven;
    }
}