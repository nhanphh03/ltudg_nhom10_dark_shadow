using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : DefeatE
{
    private bool ok = false;
    private float tRemain = 0.5f;

    public float speed = 3.0f;
    public float changeTime;
    private float timer;

    [SerializeField] private Vector2 lookdirector;
    [SerializeField] private LayerMask ground;
    private Collider2D coli;

    private bool attack = false;

    protected override void Start()
    {
        base.Start();
        coli = GetComponent<Collider2D>();
        timer = changeTime;
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
            attack = true;
            float x = hit.collider.transform.position.x - transform.position.x;
            float y = hit.collider.transform.position.y - transform.position.y;

            rb.velocity = new Vector2(x * 2, y * 3);
            anim.SetBool("Attack", true);
        }

        if (coli.IsTouchingLayers(ground))
        {
            base.JumpOn();
        }
    }

    private void FixedUpdate()
    {
        if (base.check || attack) return;
        rb.velocity = new Vector2(lookdirector.x * speed, rb.velocity.y);
        transform.localScale = new Vector2(-lookdirector.x, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.changeHealth(-6);
            anim.SetTrigger("Boom");
            ok = true;
        }

        if(collision.gameObject.tag == "Enemy")
        {
            ok = true;
        }
    }
}
