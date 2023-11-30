using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private Animator parent_animator;
    private bool can_attack = false;
    private bool in_range = false;
    private bool attacking = false;
    private GameObject player;
    public LayerMask obstacleLayer;
    void Start()
    {
        parent_animator = this.transform.parent.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (in_range)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Vector2.Distance(player.transform.position, transform.position), obstacleLayer);
            if(hit.collider == null)
            {
                can_attack = true;
            }
            else
            {
                can_attack = false;
            }

            if(can_attack && !attacking && player.GetComponent<PlayerStat>().Health > 0)
            {
                attacking = true;
                parent_animator.SetInteger("state", 2);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard"))
        {
            in_range = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard"))
        {
            can_attack = false;
            in_range = false;
            attacking = false;
            parent_animator.SetInteger("state", 1);
        }
    }
}
