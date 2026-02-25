using UnityEngine;

public class TimedSnowCloud : MonoBehaviour
{
    public float activeTime = 4f;
    private float timer;

    void Start()
    {
        timer = activeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            // تبدیل به ابر معمولی
            GetComponent<CloudBase>().isSnowCloud = false;

            // برف غیرفعال شد
            Transform snow = transform.Find("SnowArea");
            if (snow != null)
                snow.gameObject.SetActive(false);

            // این اسکریپت هم دیگه لازم نیست
            Destroy(this);
        }
    }
}
