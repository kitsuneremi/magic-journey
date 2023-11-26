using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StaffAttack : MonoBehaviour
{
    private GameObject player;
    private PlayerStat playerStat;
    public TextMeshPro damageTextPrefab;
    void Start()
    {
        player = GameObject.Find("Wizard");
        playerStat = player.GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStat enemyStat = collision.gameObject.GetComponent<EnemyStat>();
            float damage = playerStat.Attack - enemyStat.Defend;
            enemyStat.Health -= damage;
            DisplayDamageText(damage);
        }
    }


    void DisplayDamageText(float damage)
    {

        TextMeshPro damageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        damageText.text = Mathf.Ceil(damage).ToString();
        Destroy(damageText.gameObject, 1.0f);
    }
}
