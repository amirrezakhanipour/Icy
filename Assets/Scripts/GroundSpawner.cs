using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Ground Prefabs")]
    public GameObject groundGrass;
    public GameObject groundSand;
    public GameObject groundSnow;

    [Header("Spawn Settings")]
    public float chunkLength = 30f;
    public int chunksOnScreen = 6;

    // این ۳ تا برای DifficultyManager لازم هستند
    [HideInInspector] public float grassChance = 0.6f;
    [HideInInspector] public float snowChance = 0.3f;
    [HideInInspector] public float sandChance = 0.1f;

    private float spawnX = 0f;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("GroundSpawner: Player تعریف نشده!");
            enabled = false;
            return;
        }

        // ایجاد چانک‌های اولیه
        for (int i = 0; i < chunksOnScreen; i++)
        {
            if (i == 0)
                SpawnChunk(GroundType.Grass);
            else
                SpawnRandomChunk();
        }
    }

    void Update()
    {
        if (player.position.x > spawnX - (3f * chunkLength))
            SpawnRandomChunk();
    }

    enum GroundType { Grass, Sand, Snow }

    void SpawnRandomChunk()
    {
        float r = Random.value;

        GroundType type;

        if (r < grassChance)
            type = GroundType.Grass;
        else if (r < grassChance + snowChance)
            type = GroundType.Snow;
        else
            type = GroundType.Sand;

        SpawnChunk(type);
    }

    void SpawnChunk(GroundType type)
    {
        GameObject prefab = groundGrass;

        switch (type)
        {
            case GroundType.Grass: prefab = groundGrass; break;
            case GroundType.Sand: prefab = groundSand; break;
            case GroundType.Snow: prefab = groundSnow; break;
        }

        Vector3 pos = new Vector3(spawnX, 0f, 0f);
        Instantiate(prefab, pos, Quaternion.identity);

        spawnX += chunkLength;
    }
}
