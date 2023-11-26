using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hp : MonoBehaviour
{
    private float currentHp;
    [SerializeField]private HpBar healthBar;
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
            Destroy(this.gameObject);
            playerStat.Exp += enemyStat.enemyData.expGiven;
        }
    }

    private void OnDestroy()
    {
        if (transform.parent)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
