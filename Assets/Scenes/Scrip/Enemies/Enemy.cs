using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DefeatE
{
    public float speed;
    public float changeTime;
    private float timer;
    private int director = 1;
    public GameObject dialogBox;

    private bool ok = false;
    private float tRemain = 0.5f;

    protected override void Start()
    {
        base.Start();
        timer = changeTime;
    }

    void Update()
    {
        if(ok)
        {
            tRemain -= Time.deltaTime;
            if(tRemain <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (base.check)
        {
            ok = true;
            return;
        }
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            director = -director;
            timer = changeTime;
            dialogBox.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (base.check) return;
        rb.velocity = new Vector2(1 * director, rb.velocity.y);
        transform.localScale = new Vector2(1 * director, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if(player != null)
        {
            if (player != null)
            {
                int x = (int)player.state;
                if (x != 2)
                {
                    player.changeHealth(-3);
                    if (director > 0) dialogBox.SetActive(true);
                }
            }
        }
    }
}
