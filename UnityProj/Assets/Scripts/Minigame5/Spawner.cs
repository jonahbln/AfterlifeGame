using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject toSpawnObject;
    public int numberOfSpawn = 15;
    public float spawnAreaWidth = 8f;
    public float spawnAreaHeight = 5f;

    void Start()
    {
        SpawnCircles();
    }

    void SpawnCircles()
    {
        for (int i = 0; i < numberOfSpawn; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2),
                Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2)
            );
            Instantiate(toSpawnObject, randomPosition, Quaternion.identity);
        }
    }
}
