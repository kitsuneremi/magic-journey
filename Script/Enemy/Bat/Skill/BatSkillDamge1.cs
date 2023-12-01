using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSkillDamage1 : MonoBehaviour
{
/*    public GameObject damagePrefab;*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard"))
        {
            collision.gameObject.GetComponent<PlayerStat>().Health -= 90;
        }
    }
}
