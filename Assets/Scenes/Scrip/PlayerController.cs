using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    private Animator animator;
    public enum State {idle, running, jumping, falling, hurting};
    public State state = State.idle;
    private Collider2D coli;

    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask eNemy;
    [SerializeField] private float speed;
    protected int cherry = 0;
    [SerializeField] private TextMeshProUGUI cherryText;
    [SerializeField] private float hurtForece = 20.0f;
    [SerializeField] private Text Healthbar;
    private AudioSource audi;
    public AudioClip deafeatClip;
    public AudioClip hitClip;

    float horizontal;
    float vertical;

    public int maxHealth;
    private int currentHealth;
    public int getHealth { get { return currentHealth; } }

    public float timeInvinsible;
    private bool Isinvincible;
    private float timer;

    public GameObject apple;
    Vector2 lookdirector;
    private float timeCD;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coli = GetComponent<Collider2D>();
        audi = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        Healthbar.text = currentHealth.ToString();
    }

    void Update()
    {
        if (timeCD > 0) { timeCD -= Time.deltaTime; }

        if(currentHealth <= 0) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

        if(state != State.hurting)
        {
            Movement();
        }

        if (Isinvincible)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Isinvincible = false;
            }
        }

        if (Input.GetButtonDown("Jump") && timeCD <= 0)
        {
            timeCD = 1.0f;
            launch();
        }

        animationState();
        animator.SetInteger("state", (int)state);

        PlayerPrefs.SetInt("Score", cherry);
    }

    private void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (horizontal < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            lookdirector = new Vector2(-1, 0);
        }
        else if (horizontal > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            lookdirector = new Vector2(1, 0);
        }
        else
        {

        }
        if (vertical > 0 && (coli.IsTouchingLayers(ground) || coli.IsTouchingLayers(eNemy)))
        {
            Jump();
        }
    } // nhân vật di chuyển

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 40.0f);
        state = State.jumping;
    } // nhân vật nhảy

    private void animationState()
    {
        if (state == State.jumping)
        {
            if(rb.velocity.y < 0.0f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coli.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if(state == State.hurting)
        {
            if(Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2.0f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    } // các trạng thái của nhân vật

    public void changeHealth(int value)
    {
        if(value < 0)
        {
            if (Isinvincible) return;
            Isinvincible = true;
            timer = timeInvinsible;
            state = State.hurting;
            makeAudio(hitClip);
        }

        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        Healthbar.text = currentHealth.ToString();
    } // thay đổi máu

    public void launch()
    {
        GameObject pj = Instantiate(apple, rb.position + Vector2.up * 0.5f, quaternion.identity);

        Projectiles fruit = pj.gameObject.AddComponent<Projectiles>();
        fruit.audiC = deafeatClip;
        fruit.launch(lookdirector, 700);

    } // sử dụng kỹ năng

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "collectable")
        {
            Destroy(collision.gameObject);
            cherry += 1;
            cherryText.text = cherry.ToString();
        }
        if(collision.tag == "CherryKing")
        {
            Destroy(collision.gameObject);
            cherry += 100;
            cherryText.text = cherry.ToString();
        }
    } // thu nhập các trái cherry

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            DefeatE enemy = collision.gameObject.GetComponent<DefeatE>();
            if(state == State.falling)
            {
                enemy.JumpOn();
                makeAudio(deafeatClip);
                Jump();
            }
            else
            {
                state = State.hurting;
                makeAudio(hitClip);
                if(collision.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForece, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForece, rb.velocity.y);
                }
            }
        }
    } // nhân vật va chạm với các kẻ thù

    private void makeAudio(AudioClip clip)
    {
        audi.PlayOneShot(clip);
    }
}
