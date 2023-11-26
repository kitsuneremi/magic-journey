using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private Animator parent_animator;
    void Start()
    {
        parent_animator = this.transform.parent.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard"))
        {
            var player_stat = collision.gameObject.GetComponent<PlayerStat>();
            if (player_stat.Health > 0)
            {
                parent_animator.SetInteger("state", 2);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard"))
        {
            parent_animator.SetInteger("state", 1);
        }
    }
}
