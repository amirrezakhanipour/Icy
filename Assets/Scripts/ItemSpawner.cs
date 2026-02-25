using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject hatPrefab;

    public float firstSpawnDistance = 5f;
    public float spawnEveryX = 5f;
    public float spawnHeight = 1f;
    public float zOffset = 0f;

    private float nextSpawnX;

    void Start()
    {
        nextSpawnX = firstSpawnDistance;
    }

    void Update()
    {
        if (player.position.x >= nextSpawnX)
        {
            Vector3 pos = new Vector3(
                nextSpawnX,
                spawnHeight,
                zOffset
            );

            Instantiate(hatPrefab, pos, Quaternion.identity);

            nextSpawnX += spawnEveryX;
        }
    }
}
