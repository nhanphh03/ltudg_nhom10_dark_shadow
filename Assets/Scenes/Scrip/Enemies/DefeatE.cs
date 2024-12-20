using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatE : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected bool check = false;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    public void JumpOn()
    {
        anim.SetTrigger("Death");
        check = true;
    }
}
