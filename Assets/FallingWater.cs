using System.Collections;
using UnityEngine;

public class FallingWater : MonoBehaviour
{
    Vector2 startPos;
    private bool isInvulnerable = false;  // Controla si el personaje está en cooldown
    [SerializeField] private float invulnerabilityDuration = 2f;  // Duración del cooldown
    private SpriteRenderer spriteRenderer;
    AudioManager audioManager;
    private EnemyManager enemyManager;

    private void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();  // Obtén el SpriteRenderer del personaje

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
        {
            //descativa el collider y la gravedad
            GetComponent<Rigidbody2D>().gravityScale = -1;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<TouchingDirections>().enabled = false;

            Die();

        }
        else if (collision.CompareTag("Enemy1"))
        {
            if (!isInvulnerable)  // Solo recibe daño si no está en cooldown
            {
                audioManager.PlaySfx(audioManager.get_damaged);
                GetComponent<Health>().TakeDamage(1);
                StartCoroutine(ActivateInvulnerability());  // Inicia el cooldown y parpadeo
            }
        }
    }

    private void Die()
    {
        audioManager.PlaySfx(audioManager.death);
        ScoreManager.instance.ResetScore();
        GetComponent<PlayerController>().enabled = false;
        GetComponent<TouchingDirections>().enabled = false;
        //desactivar el collider y la gravedad
        GetComponent<Rigidbody2D>().gravityScale = -1;

        StartCoroutine(Respawn(0.8f));

        enemyManager.RespawnEnemies();

    }

    IEnumerator Respawn(float delay)
    {

        yield return new WaitForSeconds(delay);
        transform.position = startPos;
        GetComponent<Rigidbody2D>().gravityScale = 5;
        GetComponent<PlayerController>().enabled = true;
        GetComponent<TouchingDirections>().enabled = true;
        


        audioManager.PlaySfx(audioManager.respawn);

    }

    private IEnumerator ActivateInvulnerability()
    {
        isInvulnerable = true;  // Activa el cooldown
        float elapsed = 0f;

        while (elapsed < invulnerabilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;  // Alterna la visibilidad del personaje
            yield return new WaitForSeconds(0.1f);  // Espera 0.1 segundos antes de cambiar de nuevo
            elapsed += 0.1f;
        }

        spriteRenderer.enabled = true;  // Asegúrate de que el personaje esté visible al final del cooldown
        isInvulnerable = false;  // Desactiva el cooldown
    }
    
}
