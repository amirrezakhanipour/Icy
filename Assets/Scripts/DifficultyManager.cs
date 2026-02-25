using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("Refs")]
    public Transform player;
    public IceStats iceStats;
    public CloudSpawner cloudSpawner;
    public ItemSpawner itemSpawner;
    public GroundSpawner groundSpawner;

    [Header("Distance Settings")]
    public float distancePerLevel = 60f;
    private int currentLevel = 0;
    private float nextLevelAt = 60f;

    [Header("Sun & Snow")]
    public float baseSunDamage = 6f;
    public float sunIncreasePerLevel = 1f;

    public float baseSnowHeal = 7f;
    public float snowHealDecreasePerLevel = 0.5f;
    public float minSnowHeal = 3f;

    [Header("Cloud Spawning")]
    public float baseCloudSpawnDistance = 35f;
    public float cloudSpawnIncreasePerLevel = 4f;
    public float maxCloudSpawnDistance = 80f;

    [Header("Hat Spawning")]
    public float baseHatSpawnEveryX = 40f;
    public float hatSpawnIncreasePerLevel = 10f;
    public float maxHatSpawnEveryX = 200f;

    public float baseFirstHatSpawn = 20f;
    public float firstSpawnIncreasePerLevel = 5f;
    public float maxFirstSpawnDistance = 80f;

    [Header("Ground Difficulty")]
    public float startGrassChance = 0.6f;
    public float startSnowChance = 0.3f;
    public float startSandChance = 0.1f;

    public float grassChanceDecreasePerLevel = 0.05f;
    public float snowChanceDecreasePerLevel = 0.03f;
    public float sandChanceIncreasePerLevel = 0.08f;

    void Start()
    {
        ApplyDifficulty();
    }

    void Update()
    {
        if (player == null) return;

        float x = player.position.x;

        if (x >= nextLevelAt)
        {
            currentLevel++;
            nextLevelAt += distancePerLevel;
            ApplyDifficulty();
        }
    }

    void ApplyDifficulty()
    {
        // ---------- SUN / SNOW ----------
        if (iceStats != null)
        {
            iceStats.sunDamagePerSecond = baseSunDamage + currentLevel * sunIncreasePerLevel;

            float newSnowHeal = baseSnowHeal - currentLevel * snowHealDecreasePerLevel;
            newSnowHeal = Mathf.Max(minSnowHeal, newSnowHeal);
            iceStats.snowHealPerSecond = newSnowHeal;
        }

        // ---------- CLOUDS ----------
        if (cloudSpawner != null)
        {
            float newCloudDistance = baseCloudSpawnDistance + currentLevel * cloudSpawnIncreasePerLevel;
            newCloudDistance = Mathf.Min(maxCloudSpawnDistance, newCloudDistance);

            cloudSpawner.spawnDistance = newCloudDistance;
        }

        // ---------- HATS ----------
        if (itemSpawner != null)
        {
            float newHatSpawn = baseHatSpawnEveryX + currentLevel * hatSpawnIncreasePerLevel;
            newHatSpawn = Mathf.Min(maxHatSpawnEveryX, newHatSpawn);

            itemSpawner.spawnEveryX = newHatSpawn;
        }

        // ---------- GROUNDS ----------
        if (groundSpawner != null)
        {
            float grass = Mathf.Max(0f, startGrassChance - currentLevel * grassChanceDecreasePerLevel);
            float snow = Mathf.Max(0f, startSnowChance - currentLevel * snowChanceDecreasePerLevel);
            float sand = Mathf.Min(1f, startSandChance + currentLevel * sandChanceIncreasePerLevel);

            float sum = grass + snow + sand;
            if (sum < 0.001f) sum = 0.001f;

            groundSpawner.grassChance = grass / sum;
            groundSpawner.snowChance = snow / sum;
            groundSpawner.sandChance = sand / sum;
        }

        Debug.Log("Difficulty Level = " + currentLevel);
    }
}
