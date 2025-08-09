using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Serializable]
    public class WeightedPrefab
    {
        public Transform prefab;
        public int weight;
    }

    public static SpawnManager Instance { get; private set; }

    [Header("Spawn Settings")]
    [SerializeField] private float spawnRadiusMin;
    [SerializeField] private float spawnRadiusMax;
    [SerializeField] private float spawnTimerMin;
    [SerializeField] private float spawnTimeMax;
    [Range(100, 1000)][SerializeField] private int spawnLimit;

    [Space]
    [SerializeField] private LayerMask obstacleMask;

    [Space]
    [SerializeField] private List<WeightedPrefab> enemySpawnPrefabs;
    [SerializeField] private List<WeightedPrefab> ammoLootPrefabs;
    [SerializeField] private List<WeightedPrefab> healthLootPrefabs;

    private float currentSpawnTimer;
    private Transform playerTransform;
    private List<Transform> spawnedEnemyList;

    private void Awake()
    {
        Instance = this;
        spawnedEnemyList = new List<Transform>();

    }

    private void Start()
    {
        playerTransform = Player.Instance.transform;
        SetCurrentSpawnTimer();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            currentSpawnTimer = Mathf.Max(currentSpawnTimer -= Time.deltaTime, 0f);

            if (currentSpawnTimer <= 0f && spawnedEnemyList.Count < spawnLimit)
            {
                Transform prefab = SpawnPrefab(enemySpawnPrefabs, FindValidSpawnPosition());
                spawnedEnemyList.Add(prefab.transform);

                prefab.GetComponent<EnemyAI>().SetTarget(playerTransform);

                SetCurrentSpawnTimer();
            }
        }
    }

    public void SetCurrentSpawnTimer()
    {
        int kills = GameManager.Instance.GetEnemiesKilled();
        float minDivisor = 1f;
        float divisorStep = 1f;
        int killsPerStep = 25;
        float maxDivisor = 5f;

        float rawDivisor = minDivisor + Mathf.Floor(kills / (float)killsPerStep) * divisorStep;
        float divisor = Mathf.Min(rawDivisor, maxDivisor);

        currentSpawnTimer = UnityEngine.Random.Range(spawnTimerMin, spawnTimeMax) / divisor;
    }

    public void SpawnLoot(int spawnTime, float lootChance, Vector2 position)
    {
        for (int i = 0; i < spawnTime; i++)
        {
            if (UnityEngine.Random.value <= lootChance)
            {
                List<WeightedPrefab> chosenList = UnityEngine.Random.value <= 0.85f ? ammoLootPrefabs : healthLootPrefabs;
                SpawnPrefab(chosenList, position);
            }
        }
    }

    private Transform SpawnPrefab(List<WeightedPrefab> prefabs, Vector2 position)
    {
        Transform spawnedPrefab = Instantiate(GetRandomWeightedPrefab(prefabs), position, Quaternion.identity);
        return spawnedPrefab;
    }

    private Vector2 FindValidSpawnPosition()
    {
        Vector2 pos;
        int safety = 10;

        do
        {
            Vector2 offset =
                UnityEngine.Random.insideUnitCircle.normalized *
                UnityEngine.Random.Range(spawnRadiusMin, spawnRadiusMax);

            pos = (Vector2)transform.position + offset;
            safety--;
        }
        while (Physics2D.OverlapCircle(pos, 0.5f, obstacleMask) && safety > 0);

        return pos;
    }

    private Transform GetRandomWeightedPrefab(List<WeightedPrefab> weightedList)
    {
        int totalWeight = 0;

        foreach (WeightedPrefab item in weightedList)
            totalWeight += item.weight;

        int randomWeight = UnityEngine.Random.Range(0, totalWeight);

        foreach (WeightedPrefab item in weightedList)
        {
            if (randomWeight < item.weight)
                return item.prefab;

            randomWeight -= item.weight;
        }

        return weightedList[0].prefab;
    }

    public void RemoveEnemyFromList(Transform transform) => spawnedEnemyList.Remove(transform);
}
