using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed = 1f;
    private Rigidbody2D rb;
    private bool movingRight = true;
    private Vector3 initialPosition;

    public bool isDead = false;  // Track if the enemy is dead

    [SerializeField] private Transform groundDetection;
    private Animator anim;
    private AudioManager audioManager;

    private ScoreManager scoreManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        initialPosition = transform.position;
        isDead = false;  // Ensure the enemy is alive when the game starts

        // Find and reference the ScoreManager in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        if (isDead) return;  // Don't move the enemy if it's dead

        // Existing movement code...
        rb.velocity = new Vector2(moveSpeed * (movingRight ? 1 : -1), rb.velocity.y);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f);
        RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position, movingRight ? Vector2.right : Vector2.left, 0.1f);

        if (groundInfo.collider == false || wallInfo.collider != null)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;  // Don't take damage if already dead

        health -= damage;

        if (health <= 0)
        {
            Die();
            scoreManager.AddPoints(10);
        }
    }

    private void Die()
    {
        isDead = true;  // Mark the enemy as dead
        moveSpeed = 0;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        audioManager.PlaySfx(audioManager.enemydeath);
        anim.SetTrigger("die");
        anim.SetBool("alive", false);

        // Use Destroy to remove the GameObject after a delay
        Destroy(gameObject,0.5f);
    }

    // Reset enemy state when respawned
    public void Respawn()
    {
        isDead = false;  // Reset death flag
        health = 100;
        moveSpeed = 1f;
        transform.position = initialPosition;
        GetComponent<Rigidbody2D>().gravityScale = 5;
        GetComponent<Collider2D>().enabled = true;
        anim.SetBool("alive", true);
    }
}
