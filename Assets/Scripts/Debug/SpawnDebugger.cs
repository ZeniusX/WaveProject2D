using System.Collections.Generic;
using UnityEngine;

public class SpawnDebugger : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float spawnRadiusMin;
    [SerializeField] private float spawnRadiusMax;
    [SerializeField] private float spawnTimerMin;
    [SerializeField] private float spawnTimeMax;

    [Space]
    [SerializeField] private List<Transform> spawnTransformList;

    private Transform playerTransform;
    private float currentSpawnTimer;

    private void Start()
    {
        if (!GameManager.Instance.IsDebugMode()) gameObject.SetActive(false);

        playerTransform = Player.Instance.transform;
        currentSpawnTimer = Random.Range(spawnTimerMin, spawnTimeMax);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            currentSpawnTimer = Mathf.Max(currentSpawnTimer -= Time.deltaTime, 0f);

            if (currentSpawnTimer <= 0f)
            {
                SpawnPrefab();
                currentSpawnTimer = Random.Range(spawnTimerMin, spawnTimeMax);
            }
        }
    }

    private void SpawnPrefab()
    {
        Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(spawnRadiusMin, spawnRadiusMax);
        Vector2 spawnPos = (Vector2)transform.position + randomOffset;

        Instantiate(spawnTransformList[Random.Range(0, spawnTransformList.Count)], spawnPos, Quaternion.identity);
    }
}
