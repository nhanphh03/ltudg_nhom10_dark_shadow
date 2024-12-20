using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bettle : DefeatE
{
    public float speed;
    public float changeTime;
    private float timer;
    [SerializeField] private Vector2 director;
    private bool ok = false;
    private float tRemain = 0.5f;
    public bool vertical = false;

    protected override void Start()
    {
        base.Start();
        timer = changeTime;
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
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            director = -director;
            timer = changeTime;
        }
    }

    private void FixedUpdate()
    {
        if (base.check) return;
        if (!vertical)
        {
            rb.velocity = new Vector2(director.x * speed, rb.velocity.y);
            transform.localScale = new Vector2(-director.x, 1);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, director.y * speed);
        }
    }
}
