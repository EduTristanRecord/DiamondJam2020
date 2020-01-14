using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectibleSpawnerManager : MonoBehaviour
{
    [SerializeField] private int maxPresentAtOnce = 3;
    [SerializeField] private int maxSpawnedAtOnce = 2;

    [SerializeField] private float delayBetweenSpawns = 2f;
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private float despawnDelay = 6f;

    [SerializeField] private Spawnables spawnables;

    private List<CollectibleSpawn> spawnPositions = new List<CollectibleSpawn>();
    private List<Coroutine> spawnCoroutines = new List<Coroutine>();

    private int spawnedCount;
    private float timeSinceLastSpawn;

    private void Awake()
    {
        foreach (Transform tr in transform)
        {
            CollectibleSpawn cs = tr.GetComponent<CollectibleSpawn>();

            cs.OnRemoved += CollectibleRemoved;
            spawnPositions.Add(cs);
        }
    }

    private void Update()
    {
        Spawning();
    }

    private void OnDestroy()
    {
        foreach (Coroutine cor in spawnCoroutines)
        {
            spawnCoroutines.Remove(cor);
            StopCoroutine(cor);
        }

        foreach (Transform tr in transform)
        {
            CollectibleSpawn cs = tr.GetComponent<CollectibleSpawn>();
            cs.OnRemoved -= CollectibleRemoved;
        }
    }

    private void Spawning()
    {
        // If enough, do not spawn any

        if (spawnedCount + spawnCoroutines.Count >= maxPresentAtOnce)
        {
            timeSinceLastSpawn = 0f;
            return;
        }

        // If spot freed, start timer to refill

        timeSinceLastSpawn += Time.deltaTime;

        // While not enough time elapsed, wait

        if (timeSinceLastSpawn < delayBetweenSpawns) return;

        // After enough time elapsed, spawn until either full or we spawned the max possible per go

        timeSinceLastSpawn -= delayBetweenSpawns;
        int spawnedThisFrame = 0;
        while (spawnedCount + spawnCoroutines.Count < maxPresentAtOnce && spawnedThisFrame < maxSpawnedAtOnce)
        {
            Coroutine handler = null;
            handler = StartCoroutine(SpawnNewCollectible(() =>
            {
                spawnCoroutines.Remove(handler);
            }));
            spawnCoroutines.Add(handler);

            spawnedThisFrame++;
        }
    }

    private void CollectibleRemoved()
    {
        spawnedCount--;
    }

    public IEnumerator SpawnNewCollectible(Action terminateCallback)
    {
        yield return new WaitForSeconds(spawnDelay);

        // Select random item

        float totalRate = 0f;

        foreach (Spawnables.SpawnRate sr in spawnables.spawnRates) totalRate += sr.rate;

        float randomSelection = Random.Range(0f, totalRate);
        float incremental = 0f;

        GameObject selection = null;

        foreach (Spawnables.SpawnRate sr in spawnables.spawnRates)
        {
            incremental += sr.rate;
            if (randomSelection <= incremental)
            {
                selection = sr.spawnable;
                break;
            }
        }

        // Select random spawn position

        List<CollectibleSpawn> emptySpawns = new List<CollectibleSpawn>();

        foreach (CollectibleSpawn colSp in spawnPositions) if (colSp.containedCollectible == null) emptySpawns.Add(colSp);

        CollectibleSpawn cs = spawnPositions[Random.Range(0, emptySpawns.Count)];
        Vector2 randomSpawnPosition = cs.transform.position;

        // Spawn at position

        Collectible col = Instantiate(selection, randomSpawnPosition, Quaternion.identity).GetComponent<Collectible>();

        cs.SetCollectible(col);
        cs.DespawnAfter(despawnDelay);

        spawnedCount++;
        terminateCallback?.Invoke();
    }
}
