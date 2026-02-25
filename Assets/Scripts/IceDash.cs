using UnityEngine;

public class IceDash : MonoBehaviour
{
    public float dashForce = 15f;
    public float dashCostHP = 8f;
    public float dashCostSize = 5f;
    public float cooldown = 1f;

    private float lastDashTime = 0f;
    private Rigidbody rb;
    private IceStats stats;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<IceStats>();
    }

    public void DoDash()
    {
        if (Time.time < lastDashTime + cooldown)
            return;

        lastDashTime = Time.time;

        rb.AddForce(Vector3.right * dashForce, ForceMode.Impulse);

        stats.HP -= dashCostHP;
        stats.Size -= dashCostSize;
    }
}
