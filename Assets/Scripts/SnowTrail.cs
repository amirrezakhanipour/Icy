using UnityEngine;

public class SnowTrail : MonoBehaviour
{
    public GameObject stampPrefab;    // ÎæÏö SnowStamp
    public float distanceBetweenStamps = 1f;

    private Vector3 lastPos;

    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, lastPos);

        if (dist >= distanceBetweenStamps)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, 0f, transform.position.z);

            Instantiate(stampPrefab, spawnPos, Quaternion.Euler(90, 0, 0));

            lastPos = transform.position;
        }
    }
}
