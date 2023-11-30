using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Ground"))
        {
            GameObject wizard = GameObject.Find("Wizard");
            Animator anim = wizard.GetComponent<Animator>();
            Movement m = wizard.GetComponent<Movement>();
            m.ResetJumping();
            anim.SetBool("isJump", false);
        }
    }
}
