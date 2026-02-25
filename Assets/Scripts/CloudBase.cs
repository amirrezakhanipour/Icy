using UnityEngine;

public class CloudBase : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float shadowWidth = 3f;
    public bool isSnowCloud = false;

    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}
