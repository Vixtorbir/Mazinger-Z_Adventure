using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Enemy prefab to spawn
    public List<Transform> spawnPoints; // List of spawn points for enemies

    // We store enemies in a dictionary with spawn points as keys
    private Dictionary<Transform, GameObject> enemies = new Dictionary<Transform, GameObject>();

    private void Start()
    {
        // Spawn enemies at each spawn point initially
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnEnemy(spawnPoint);
        }
    }

    // Spawn enemy at a specific spawn point
    private void SpawnEnemy(Transform spawnPoint)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemies[spawnPoint] = newEnemy;  // Track enemy in the dictionary
    }

    // Respawn enemies when the player dies, but only if they are dead
    public void RespawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Check if the enemy at this spawn point is null (destroyed) or dead
            if (enemies[spawnPoint] == null || enemies[spawnPoint].GetComponent<Enemy>().isDead)
            {
                // Respawn enemy at the corresponding spawn point
                SpawnEnemy(spawnPoint);
            }
        }
    }
}
