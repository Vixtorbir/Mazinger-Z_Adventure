using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    
    private Animator anim;

    AudioManager audioManager;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    [SerializeField]
    private bool _alive = false;

    public bool alive
    {
        get
        {
            return _alive;
        }
        private set
        {
            _alive = value;
            anim.SetBool("alive", value);
        }
    }

    //if player is looking to the right the bullet will go to the right
    //if player is looking to the left the bullet will go to the left
    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 0)
        {
            
            rb.velocity = -transform.right * speed;
        }
        else
        {
            rb.velocity = transform.right * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {

            audioManager.PlaySfx(audioManager.hit);

            enemy.TakeDamage(damage);

            anim.SetTrigger("hit");

            anim.SetBool("alive", false);


            Destroy(gameObject);

        }

        if (hitInfo.CompareTag("ground"))
        {

            anim.SetTrigger("hit");

            anim.SetBool("alive", false);

            Destroy(gameObject);

        }

        
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
