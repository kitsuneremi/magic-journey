using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigVision : MonoBehaviour
{
    public GameObject pigBody;
    public GameObject BossUi;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard"))
        {
            pigBody.GetComponent<PigAttack>().CanFollow();
            BossUi.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard"))
        {
            pigBody.GetComponent<PigAttack>().CantFollow();
        }
    }
}
