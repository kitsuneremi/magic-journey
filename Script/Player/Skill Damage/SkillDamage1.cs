using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillDamage1 : MonoBehaviour
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
            float damage = (100/100 * playerStat.playerData.attack + 120) - enemyStat.enemyData.defend;
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
