using UnityEngine;

public class HatPickup : MonoBehaviour
{
    public float immunityTime = 6f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IceStats stats = other.GetComponent<IceStats>();

            if (stats != null)
            {
                stats.StartCoroutine(stats.StartSunImmunity(immunityTime));
            }

            Destroy(gameObject); // کلاه حذف می‌شود
        }
    }
}
