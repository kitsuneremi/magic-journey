using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    private bool fightBoss = false;
    void Update()
    {
        if (!fightBoss)
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("Boss Fight"))
        {
            Debug.Log("on"); 
            fightBoss = true;
            transform.position = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Boss Fight"))
        {
            fightBoss = false;
        }
    }
}
