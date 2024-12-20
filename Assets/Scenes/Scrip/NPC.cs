using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float displayTime = 7.0f;
    public GameObject dialogBox;
    float timer = 7.0f;
    Vector2 lookdirector = new Vector2(-1, 0);
    private Rigidbody2D rb;
    [SerializeField] private float force = 5.5f;
    private Animator anim;
    public bool ok;
    private bool check;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 1.0f && ok && check)
        {
            anim.SetTrigger("Ok");
        }

        if (timer < 0)
        {
            dialogBox.SetActive(false);
            if(ok && check)
            {
                Destroy(gameObject);
            }
        }
        RaycastHit2D hit = Physics2D.Raycast(rb.position, lookdirector, force, LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
            timer = displayTime;
            dialogBox.SetActive(true);
            check = true;
        }
    }
}
