using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillDamage2 : MonoBehaviour
{
    [SerializeField] private int lifeTimes = 3;

    private GameObject player;
    private PlayerStat playerStat;
    public TextMeshPro damageTextPrefab;
    void Start()
    {
        player = GameObject.Find("Wizard");
        playerStat = player.GetComponent<PlayerStat>();
    }

    private void Awake()
    {
        Destroy(gameObject, lifeTimes);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyable"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStat enemyStat = collision.gameObject.GetComponent<EnemyStat>();
            float damage = (30/100 * playerStat.playerData.attack + 300) - enemyStat.enemyData.defend;
            if (damage <= 0)
            {
                damage = 1;
            }
            enemyStat.Health -= damage;
            DisplayDamageText(damage);
            Destroy(gameObject);
        }
    }

    void DisplayDamageText(float damage)
    {

        TextMeshPro damageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        damageText.text = Mathf.Ceil(damage).ToString();
        Destroy(damageText.gameObject, 1.0f);
    }
}
