using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : DefeatE
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float jumplenght = 10.0f;
    [SerializeField] private float jumphight = 15.0f;
    [SerializeField] private LayerMask ground;

    private Collider2D colli;

    private bool facingleft = true;

    [SerializeField] private float timeIdle = 1.0f;
    float timer;

    private bool ok = false;
    private float tRemain = 0.5f;

    protected override void Start()
    {
        base.Start();
        colli = GetComponent<Collider2D>();
        timer = timeIdle;
    }

    void Update()
    {
        if (ok)
        {
            tRemain -= Time.deltaTime;
            if(tRemain <= 0 )
            {
                Destroy(gameObject);
            }
        }
        if (base.check)
        {
            ok = true;
            return;
        }
        if (anim.GetBool("Jump"))
        {
            if(rb.velocity.y < 0.1f)
            {
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", true);
            }
        }
        if(anim.GetBool("Fall") && colli.IsTouchingLayers(ground))
        {
            anim.SetBool("Fall", false);
        }
    }

    private void FixedUpdate()
    {
        if (base.check) return;
        timer -= Time.deltaTime;
        if(timer < 0.0f)
        {
            Move();
            anim.SetBool("Jump", true);
            timer = timeIdle;
        }
    }

    private void Move()
    {
        if (facingleft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector2(1, 1);
                }
                if (colli.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumplenght, jumphight);
                }
            }
            else
            {
                facingleft = false;
            }
        }
        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                if (colli.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumplenght, jumphight);
                }
            }
            else
            {
                facingleft = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            int x = (int)player.state;
            if (x != 2) player.changeHealth(-1);
        }
    }
}
