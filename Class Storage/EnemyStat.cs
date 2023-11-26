using System;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public float health;
    public float attack;
    public int level;
    public float defend;
    public int expGiven;

    public EnemyData(float health, float attack, int level, float defend, int expGiven)
    {
        this.health = health;
        this.attack = attack;
        this.level = level;
        this.defend = defend;
        this.expGiven = expGiven;
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

    private void Start()
    {
        Health = enemyData.health;
        Attack = enemyData.attack;
        Level = enemyData.level;
        Defend = enemyData.defend;
        ExpGiven = enemyData.expGiven;
    }

    public void SaveEnemyData()
    {
        // Convert PlayerData thành chuỗi JSON
        string json = JsonUtility.ToJson(enemyData);

        // Lưu vào PlayerPrefs hoặc file khác
        PlayerPrefs.SetString("EnemyData", json);
        PlayerPrefs.Save();
    }

    public void LoadEnemyData()
    {
        // Lấy chuỗi JSON từ PlayerPrefs hoặc file khác
        string json = PlayerPrefs.GetString("EnemyData");

        // Chuyển đổi chuỗi JSON thành PlayerData
        enemyData = JsonUtility.FromJson<EnemyData>(json);
    }
}

// Trong một script khác
/*void Start()
{
    // Tìm kẻ địch bằng tên hoặc bằng cách khác
    GameObject enemyObject = GameObject.Find("EnemyObjectName");

    if (enemyObject != null)
    {
        Enemy enemyComponent = enemyObject.GetComponent<Enemy>();

        if (enemyComponent != null)
        {
            // Sử dụng thông tin của kẻ địch
            string enemyName = enemyComponent.enemyData.enemyName;
            int enemyHealth = enemyComponent.enemyData.health;
            int enemyDamage = enemyComponent.enemyData.damage;
            int enemyExperience = enemyComponent.enemyData.experience;
        }
    }
}
*/