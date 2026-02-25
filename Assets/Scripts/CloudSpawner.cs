using UnityEngine;
using System.Collections.Generic;

public class CloudSpawner : MonoBehaviour
{
    public Transform player;

    [Header("Cloud Prefabs")]
    public GameObject normalCloud;
    public GameObject fastCloud;
    public GameObject heavyCloud;
    public GameObject limitedSnowCloud;
    public GameObject timedSnowCloud;

    [Header("Spawn Settings")]
    public float spawnDistance = 35f;   // فاصله استاندارد بین ابرها
    public float cloudHeight = 3f;      // ارتفاع ثابت ابر روی مسیر
    public float slightZOffset = 0.3f;  // پخش بسیار کوچک چپ/راست (0 = دقیق وسط)

    [Header("Performance")]
    public int maxActiveClouds = 20;    // سقف تعداد ابرها در صحنه

    private float nextSpawnX = 0f;
    private List<GameObject> activeClouds = new List<GameObject>();

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("CloudSpawner: Player تنظیم نشده!");
            enabled = false;
            return;
        }

        // اولین نقطه ساخت ابر (کمی جلوتر از یخ)
        nextSpawnX = player.position.x + 30f;
    }

    void Update()
    {
        // هر وقت پلیر نزدیک به SpawnPoint شد → ابر جدید
        if (player.position.x > nextSpawnX - 25f)
        {
            SpawnCloud();
            nextSpawnX += spawnDistance;
        }

        CleanupClouds();
    }

    void SpawnCloud()
    {
        GameObject prefab = ChooseCloud();
        if (prefab == null) return;

        Vector3 pos = new Vector3(
            nextSpawnX,
            cloudHeight,
            Random.Range(-slightZOffset, slightZOffset)  // نزدیک وسط، نه مسخره‌بازی -3 تا +3
        );

        GameObject cloud = Instantiate(prefab, pos, Quaternion.identity);
        activeClouds.Add(cloud);
    }

    void CleanupClouds()
    {
        for (int i = activeClouds.Count - 1; i >= 0; i--)
        {
            if (activeClouds[i] == null)
            {
                activeClouds.RemoveAt(i);
                continue;
            }

            // ابرهایی که خیلی عقب موندن حذف می‌شن
            if (activeClouds[i].transform.position.x < player.position.x - 40f)
            {
                Destroy(activeClouds[i]);
                activeClouds.RemoveAt(i);
            }
        }

        // اگر ابرها زیادی زیاد شدن، قدیمی‌ترین رو حذف کن
        if (activeClouds.Count > maxActiveClouds)
        {
            Destroy(activeClouds[0]);
            activeClouds.RemoveAt(0);
        }
    }

    GameObject ChooseCloud()
    {
        int r = Random.Range(0, 100);

        if (r < 40) return normalCloud;
        if (r < 60) return fastCloud;
        if (r < 75) return heavyCloud;
        if (r < 95) return limitedSnowCloud;   // 20%
        return timedSnowCloud;                 // 5%

    }
}
