using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] Transform hands, body, whole;

    [Header("Leaning")]
    public float handTiltMultiplier = -10f;
    public float bodyTiltMultiplier = 4f;
    public float leanSmoothing = 10f;

    [Header("Squash & Stretch")]
    public float squashAmount = 0.1f;
    public float stretchAmount = 0.05f;
    public float squashSmooth = 10f;

    [Header("Bobbing")]
    public float bobSpeed = 12f;
    public float bobAmount = 0.03f;

    private Vector3 baseScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseScale = whole.localScale;
    }

    void Update()
    {
        float vx = rb.linearVelocity.x;
        float vy = rb.linearVelocity.y;
        float speed = rb.linearVelocity.magnitude;

        // -------------------------
        // SMOOTH TILT
        // -------------------------
        float handTilt = vx * handTiltMultiplier;
        float bodyTilt = vx * bodyTiltMultiplier;

        hands.rotation = Quaternion.Lerp(
            hands.rotation,
            Quaternion.Euler(0, 0, handTilt),
            Time.deltaTime * leanSmoothing
        );

        body.rotation = Quaternion.Lerp(
            body.rotation,
            Quaternion.Euler(0, 0, bodyTilt),
            Time.deltaTime * leanSmoothing
        );

        // -------------------------
        // JUMP SQUASH / STRETCH
        // -------------------------
        float targetX = baseScale.x * (1 - vy * squashAmount);
        float targetY = baseScale.y * (1 + vy * stretchAmount);

        Vector3 targetScale = new Vector3(
            Mathf.Clamp(targetX, baseScale.x * 0.75f, baseScale.x * 1.2f),
            Mathf.Clamp(targetY, baseScale.y * 0.75f, baseScale.y * 1.2f),
            1
        );

        whole.localScale = Vector3.Lerp(
            whole.localScale,
            targetScale,
            Time.deltaTime * squashSmooth
        );

        // -------------------------
        // SUBTLE BOBBING WHEN MOVING
        // -------------------------
        if (speed > 0.1f)
        {
            float bob = Mathf.Sin(Time.time * bobSpeed) * bobAmount;
            whole.localPosition = new Vector3(0, bob, 0);
        }
        else
        {
            // Smoothly return to neutral
            whole.localPosition = Vector3.Lerp(
                whole.localPosition,
                Vector3.zero,
                Time.deltaTime * 8f
            );
        }
    }
}
