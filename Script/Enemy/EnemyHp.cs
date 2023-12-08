using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    private float currentHp;
    [SerializeField] private EnemyHpBar healthBar;
    [SerializeField] private TextMeshProUGUI levelUi;

    private EnemyStat enemyStat;
    private GameObject player;
    private PlayerStat playerStat;
    private PlayerExp playerExp;
    void Start()
    {
        enemyStat = GetComponent<EnemyStat>();
        healthBar = GetComponent<EnemyHpBar>();
        enemyStat.Health = enemyStat.listPhase[enemyStat.CurrentPhase].health;
        healthBar.SetMaxHealth(enemyStat.listPhase[enemyStat.CurrentPhase].health);
        levelUi.text = "Lv " + enemyStat.listPhase[enemyStat.CurrentPhase].level;
        player = GameObject.Find("Wizard");
        playerStat = player.GetComponent<PlayerStat>(); 
        playerExp = player.GetComponent<PlayerExp>();
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHp = enemyStat.Health;
        healthBar.SetHealth(currentHp);
        if (currentHp <= 0)
        {
            DropItem drop = GetComponent<DropItem>();
            drop.InstantiateItem(this.transform);
            Destroy(this.gameObject);
            playerStat.Exp += enemyStat.listPhase[enemyStat.CurrentPhase].expGiven;
            playerExp.CalculateLevel();
        }
    }

    private void OnDestroy()
    {
        if (this.transform.parent)
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
