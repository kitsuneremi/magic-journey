using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimation : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void CellIn()
    {
        anim.SetBool("isFlying", false);
        anim.SetTrigger("idle");
    }

    public void CellOut()
    {
        anim.SetBool("isFlying", true);
    }
}
