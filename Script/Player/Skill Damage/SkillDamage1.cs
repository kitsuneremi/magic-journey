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
            Debug.Log(playerStat.playerData.attack + " " + enemyStat.listPhase[enemyStat.CurrentPhase].defend);
            float damage = (10.0f / 100.0f * playerStat.playerData.attack + 120.0f) - enemyStat.listPhase[enemyStat.CurrentPhase].defend;

            Debug.Log(damage);
            if(damage <= 0)
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
