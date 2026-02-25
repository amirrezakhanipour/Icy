using UnityEngine;
using System.Collections;   // برای IEnumerator لازم است

public class IceStats : MonoBehaviour
{
    [Header("Stats")]
    public float HP = 100f;
    public float Size = 100f;

    [Header("Sun Damage Base")]
    public float sunDamagePerSecond = 8f;

    [Header("Snow Heal")]
    public float snowHealPerSecond = 20f;

    private bool inShade = false;
    private bool inSnow = false;

    private Rigidbody rb;

    private string currentGround = "Grass";

    private bool sunImmune = false;

    [Header("Hat Visual")]
    public GameObject hatVisualPrefab;

    [Header("Sweat FX")]
    public ParticleSystem sweatEffect;

    private float shadeTimer = 0f;
    public float shadeLimit = 8f;

    private float snowTimer = 0f;
    public float snowLimit = 8f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (sweatEffect != null)
            sweatEffect.Stop();
    }

    void Update()
    {
        CheckGround();

        float groundMultiplier = 1f;

        switch (currentGround)
        {
            case "Sand": groundMultiplier = 1.6f; break;
            case "Grass": groundMultiplier = 1f; break;
            case "Snow": groundMultiplier = 0.6f; break;
        }

        // ---------------- Anti-camp Shade ----------------
        if (inShade)
        {
            shadeTimer += Time.deltaTime;
            if (shadeTimer >= shadeLimit)
                inShade = false;
        }
        else shadeTimer = 0f;

        // ---------------- Anti-camp Snow -----------------
        if (inSnow)
        {
            snowTimer += Time.deltaTime;
            if (snowTimer >= snowLimit)
                inSnow = false;
        }
        else snowTimer = 0f;

        // ---------------- Sun Damage ---------------------
        if (!sunImmune && !inShade && !inSnow)
        {
            HP -= sunDamagePerSecond * groundMultiplier * Time.deltaTime;
            Size -= sunDamagePerSecond * 0.7f * groundMultiplier * Time.deltaTime;
        }

        // ---------------- Snow Heal (نسخه جدید زمان‌دار) ----------------------
        if (inSnow)
        {
            // هر ثانیه زیر برف = heal قوی‌تر
            float healMultiplier = 1f + (snowTimer * 0.35f);  // هر ثانیه +15٪ قدرت بیشتر

            HP += snowHealPerSecond * healMultiplier * Time.deltaTime;
            Size += snowHealPerSecond * 0.6f * healMultiplier * Time.deltaTime;
        }

        HP = Mathf.Clamp(HP, 0, 100);
        Size = Mathf.Clamp(Size, 5, 100);

        transform.localScale = Vector3.one * (Size / 100f);
        rb.mass = 1f + (Size / 40f);

        // ⭐⭐⭐ سیستم عرق همانی است که خواستی ⭐⭐⭐
        if (sweatEffect != null)
        {
            bool hpLow = HP < 80f;
            bool noShade = !inShade;
            bool noSnow = !inSnow;
            bool noHat = !sunImmune;

            bool shouldSweat = hpLow && noShade && noSnow && noHat;

            if (shouldSweat)
            {
                float melt = 80f - HP;
                float rate = Mathf.Clamp(melt * 1.2f, 5f, 50f);

                var emission = sweatEffect.emission;
                emission.rateOverTime = rate;

                if (!sweatEffect.isPlaying)
                    sweatEffect.Play();
            }
            else
            {
                var emission = sweatEffect.emission;
                emission.rateOverTime = 0;

                if (sweatEffect.isPlaying)
                    sweatEffect.Stop();
            }
        }

        if (HP <= 0)
            Die();
    }

    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
            currentGround = hit.collider.tag;
    }

    public void SetShade(bool state)
    {
        inShade = state;
    }

    public void SetSnow(bool state)
    {
        inSnow = state;
    }

    public IEnumerator StartSunImmunity(float time)
    {
        sunImmune = true;

        GameObject hat = null;

        if (hatVisualPrefab != null)
        {
            hat = Instantiate(
                hatVisualPrefab,
                transform.position + new Vector3(0, 1.2f, 0),
                Quaternion.identity
            );

            hat.transform.SetParent(transform);
        }

        yield return new WaitForSeconds(time);

        sunImmune = false;

        if (hat != null)
            Destroy(hat);
    }

    void Die()
    {
        FindObjectOfType<GameOverManager>().TriggerGameOver();
        GetComponent<SwipeController>().enabled = false;
        Debug.Log("ICE IS DEAD");
    }
}
