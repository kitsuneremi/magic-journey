using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnightHp : MonoBehaviour
{
    private float currentHp;
    [SerializeField] private KnightHpBar healthBar;
    [SerializeField] private TextMeshProUGUI levelUi;

    private Animator animator;
    private Rigidbody2D rigid;
    [SerializeField] private EnemyStat enemyStat;
    private GameObject player;
    private PlayerStat playerStat;
    void Start()
    {
/*        animator = GetComponent<Animator>();*/
        rigid = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(enemyStat.enemyData.health);
        levelUi.text = "Lv " + enemyStat.enemyData.level;
        player = GameObject.Find("Wizard");
        playerStat = player.GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHp = enemyStat.Health;
        healthBar.SetHealth(currentHp);
        if(currentHp <= 0)
        {
            DropItem drop = GetComponent<DropItem>();
            drop.InstantiateItem(this.transform);
            Destroy(this.gameObject);
            playerStat.Exp += enemyStat.enemyData.expGiven;
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
