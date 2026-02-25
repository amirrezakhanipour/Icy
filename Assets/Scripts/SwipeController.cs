using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public float swipeForce = 7f;
    public float minSwipeDistance = 50f;
    public float swipeCooldown = 0.15f;

    private Rigidbody rb;
    private Vector2 startTouchPos;
    private float lastSwipeTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // موبایل
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
                startTouchPos = t.position;

            if (t.phase == TouchPhase.Ended)
            {
                Vector2 delta = t.position - startTouchPos;

                if (Mathf.Abs(delta.x) > minSwipeDistance)
                {
                    TrySwipe(delta.x);
                }
            }
        }

        // دسکتاپ (برای تست)
        if (Input.GetMouseButtonDown(0))
            startTouchPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 delta = (Vector2)Input.mousePosition - startTouchPos;

            if (Mathf.Abs(delta.x) > minSwipeDistance)
                TrySwipe(delta.x);
        }
    }

    void TrySwipe(float deltaX)
    {
        if (Time.time - lastSwipeTime < swipeCooldown)
            return;

        lastSwipeTime = Time.time;

        Vector3 forceDir = (deltaX > 0) ? Vector3.right : Vector3.left;
        rb.AddForce(forceDir * swipeForce, ForceMode.Impulse);
    }
}
