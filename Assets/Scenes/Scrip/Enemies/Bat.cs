using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Bat : DefeatE
{
    private bool ok = false;
    private float tRemain = 0.5f;
    private float speed = 3.0f;
    private Vector2 lookdirector = new Vector2(1, 0);
    private bool fly = false;
    private float changeTime = 3.0f;
    private float timer;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (ok)
        {
            tRemain -= Time.deltaTime;
            if (tRemain <= 0)
                Destroy(gameObject);
        }
        if (base.check)
        {
            ok = true;
            return;
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            lookdirector = new Vector2(-lookdirector.x, 0);
            timer = changeTime;
        }

        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down * 0.5f, 25.0f, LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
            fly = true;
            anim.SetBool("Move", true);
        }

        if(fly)
        {
            Move();
        }
    }

    private void Move()
    {
        if (base.check) return;
        rb.velocity = new Vector2(-lookdirector.x * speed, rb.velocity.y);
        transform.localScale = new Vector2(-lookdirector.x, 1);
    }
}
