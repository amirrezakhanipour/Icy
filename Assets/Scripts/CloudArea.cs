using UnityEngine;

public class CloudArea : MonoBehaviour
{
    public enum AreaType { Shade, Snow }
    public AreaType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IceStats stats = other.GetComponent<IceStats>();
            if (type == AreaType.Shade) stats.SetShade(true);
            if (type == AreaType.Snow) stats.SetSnow(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IceStats stats = other.GetComponent<IceStats>();
            if (type == AreaType.Shade) stats.SetShade(false);
            if (type == AreaType.Snow) stats.SetSnow(false);
        }
    }
}
