using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : DefeatE
{
    private bool ok = false;
    private float tRemain = 0.5f;

    Vector2 lookdirector = new Vector2(-1, 0);
    float timer;
    float timechange = 0.5f;

    public float force = 15.0f;
    public int damage = -3;

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
            {
                Destroy(gameObject);
            }
        }
        if (base.check)
        {
            ok = true;
            return;
        }

        if (!anim.GetBool("Move"))
        {
            timechange -= Time.deltaTime;
        }

        if(timechange <= 0)
        {
            timechange = 0.5f;
            lookdirector = new Vector2(-lookdirector.x, 0);
            transform.localScale = new Vector2(-transform.localScale.x, 1);
        }

        RaycastHit2D hit = Physics2D.Raycast(rb.position, lookdirector, force, LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
            float x = hit.collider.transform.position.x - transform.position.x;
            float y = hit.collider.transform.position.y - transform.position.y;

            rb.velocity = new Vector2(x, y * 2);
            anim.SetBool("Move", true);
            timer = 2.0f;
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                anim.SetBool("Move", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            int x = (int)player.state;
            if(x != 2) player.changeHealth(damage);
        }
    }
}
