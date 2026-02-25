using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image HPFill;      // تصویر پر شونده
    public IceStats Stats;    // اسکریپت یخ

    void Update()
    {
        if (Stats == null || HPFill == null) return;

        float fillAmount = Stats.HP / 100f;

        HPFill.fillAmount = fillAmount;
    }
}
