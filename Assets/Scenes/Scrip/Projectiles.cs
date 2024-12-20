using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    Rigidbody2D rb;
    private AudioSource audi;
    public AudioClip audiC;
    private Animator anim;

    private float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
        audi = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        timer = 3.0f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 1.0f) anim.SetTrigger("Ok");
        if (timer < 0)
        {
            Destroy(gameObject);
        }
        //if(transform.position.magnitude > 1000.0f)
        //{
        //    Destroy(gameObject);
        //}
    }

    public void launch(Vector2 director, float value)
    {
        rb.AddForce(director * value);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DefeatE enemy = collision.gameObject.GetComponent<DefeatE>();

        if(collision.gameObject.tag == "Enemy")
        {
            audi.PlayOneShot(audiC);
            enemy.JumpOn();
        }
    }
}
