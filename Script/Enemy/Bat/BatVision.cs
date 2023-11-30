using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatVision : MonoBehaviour
{

    [SerializeField] private Transform defaultLocation;
    private bool needMove = false;
    private GameObject batBody;
    private bool stillIn = false;

    private void FixedUpdate()
    {
        if (needMove)
        {
            if (Vector2.Distance(batBody.transform.position, defaultLocation.position) > 0.1f)
            {
                batBody.GetComponent<Animator>().SetBool("isFlying", true);
                MoveIn();
            }
            else
            {
                needMove = false;
                batBody.GetComponent<Animator>().SetTrigger("in");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard"))
        {
            stillIn = true;
            transform.parent.GetChild(0).GetComponent<Animator>().SetTrigger("out");
            transform.parent.GetChild(0).GetComponent<BatAttack>().CanFollow();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard") && !needMove)
        {
            stillIn = false;
            needMove = true;
            batBody = transform.parent.GetChild(0).gameObject;
            transform.parent.GetChild(0).GetComponent<BatAttack>().CantFollow();

        }
        if(collision.gameObject.name.Equals("Bat Body") && !stillIn)
        {
            needMove = true;
            transform.parent.GetChild(0).GetComponent<BatAttack>().CantFollow();
            batBody = collision.gameObject;
        }
    }

    private void MoveIn()
    {
        batBody.transform.position =  Vector2.MoveTowards(batBody.transform.position, defaultLocation.position, Time.deltaTime * 4f);
    }
}
