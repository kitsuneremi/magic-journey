using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
/*    [SerializeField] private EnemyStat enemy_stat;*/
    private PlayerStat player_stat;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void Awake()
    {
        Destroy(this.gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard"))
        {
            player_stat = collision.gameObject.GetComponent<PlayerStat>();
            var player_animator = collision.gameObject.GetComponent<Animator>();
            player_animator.SetTrigger("hurt");
            player_stat.Health -= 1.2f * (120 - player_stat.Defend) + 120;
            Destroy(this.gameObject);
        }
    }
}
