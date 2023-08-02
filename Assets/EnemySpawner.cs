using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 10;
    public float spawnDelay = 2f;
    public float spawnRadius = 10f;

    private int currentEnemies = 0;
    private SpriteRenderer spawnerArea;

    void Start()
    {
        spawnerArea = GetComponent<SpriteRenderer>();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemies < maxEnemies)
            {
                Vector2 randomPoint = GetRandomPointInBounds();
                Instantiate(enemyPrefab, randomPoint, Quaternion.identity);
                currentEnemies++;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private Vector2 GetRandomPointInBounds()
    {
        Bounds bounds = spawnerArea.bounds;
        Vector2 randomPoint = new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
        return randomPoint;
    }

    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}
