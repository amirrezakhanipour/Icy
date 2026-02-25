using UnityEngine;

public class LimitedSnowCloud : MonoBehaviour
{
    public int charges = 5;

    public void UseCharge()
    {
        charges--;
        if (charges <= 0)
        {
            // وقتی شارژ تموم شد تبدیل به ابر معمولی بشه
            GetComponent<CloudBase>().isSnowCloud = false;

            // SnowArea رو خاموش کن
            Transform snow = transform.Find("SnowArea");
            if (snow != null)
                snow.gameObject.SetActive(false);
        }
    }
}
