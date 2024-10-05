using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    Vector2 startPos;
    AudioManager audioManager;

    private EnemyManager enemyManager;  // Reference to the EnemyManager

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        startPos = transform.position;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Find and reference the EnemyManager in the scene
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            // Play death animation
            anim.SetTrigger("die");
            Die();

            // Notify the EnemyManager to respawn the enemy
            
            enemyManager.RespawnEnemies();
            
        }
    }

    private void Die()
    {
        audioManager.PlaySfx(audioManager.death);
        ScoreManager.instance.ResetScore();
        GetComponent<PlayerController>().enabled = false;
        GetComponent<TouchingDirections>().enabled = false;

        StartCoroutine(Respawn(2f));
    }

    IEnumerator Respawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = startPos;
        currentHealth = startingHealth;
        GetComponent<PlayerController>().enabled = true;
        GetComponent<TouchingDirections>().enabled = true;
        anim.Play("player_idle");

        audioManager.PlaySfx(audioManager.respawn);
    }
}
