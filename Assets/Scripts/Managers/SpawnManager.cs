using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float spawnRadiusMin;
    [SerializeField] private float spawnRadiusMax;
    [SerializeField] private float spawnTimerMin;
    [SerializeField] private float spawnTimeMax;

    [Space]
    [SerializeField] private List<Transform> spawnPrefabs;

    private float currentSpawnTimer;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = Player.Instance.transform;

        currentSpawnTimer = Random.Range(spawnTimerMin, spawnTimeMax);

        SpawnPrefab();
    }

    private void Update()
    {
        currentSpawnTimer = Mathf.Max(currentSpawnTimer -= Time.deltaTime, 0f);

        if (currentSpawnTimer <= 0f)
        {
            SpawnPrefab();
            currentSpawnTimer = Random.Range(spawnTimerMin, spawnTimeMax);
        }
    }

    private void SpawnPrefab()
    {
        Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(spawnRadiusMin, spawnRadiusMax);
        Vector2 spawnPos = (Vector2)transform.position + randomOffset;

        Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Count)], spawnPos, Quaternion.identity);
    }
}
