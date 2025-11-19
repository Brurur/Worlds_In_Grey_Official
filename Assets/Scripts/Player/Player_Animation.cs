using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using YkinikY;
using FirstGearGames.SmoothCameraShaker;

public class Player_Animation : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] Transform hands, body, whole;
    [SerializeField] GameObject handNormal, handSprout;

    [Header("Leaning")]
    [SerializeField] float handTiltMultiplier = -10f;
    [SerializeField] float bodyTiltMultiplier = 4f;
    [SerializeField] float leanSmoothing = 10f;

    [Header("Squash & Stretch")]
    [SerializeField] float squashAmount = 0.1f;
    [SerializeField] float stretchAmount = 0.05f;
    [SerializeField] float squashSmooth = 10f;

    [Header("Bobbing")]
    [SerializeField] float bobSpeed = 12f;
    [SerializeField] float bobAmount = 0.03f;

    [Header("Idle Breathing")]
    [SerializeField] float idleBreathSpeed = 1.5f;
    [SerializeField] float idleBreathScale = 0.03f;
    [SerializeField] float idleBreathTilt = 2f;

    [Header("Big Boom!")]
    [SerializeField] Transform transformMask;
    [SerializeField] GameObject[] environments;
    [SerializeField] Volume greenVolume;
    [SerializeField] Transform playerCamera;
    [SerializeField] TypeWriter levelFinishedText;
    [SerializeField] ShakeData bigShake;

    private Vector3 baseScale;
    private Audio_Manager audio_Manager;
    private CanvasGroup fadeOverlay;
    private bool finishedLevel = false;
    private PlayerController_ykiniky playerController;

    void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody2D>();
        baseScale = whole.localScale;
        audio_Manager = GameObject.Find("Audio Manager").GetComponent<Audio_Manager>();
        fadeOverlay = GameObject.Find("Fade Overlay").GetComponent<CanvasGroup>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController_ykiniky>();
    }

    void Update()
    {
        float vx = rb.linearVelocity.x;
        float vy = rb.linearVelocity.y;
        float speed = rb.linearVelocity.magnitude;

        // MOVEMENT TILT
        float handTilt = vx * handTiltMultiplier;
        float bodyTilt = vx * bodyTiltMultiplier;

        hands.rotation = Quaternion.Lerp(hands.rotation, Quaternion.Euler(0, 0, handTilt), Time.deltaTime * leanSmoothing);

        body.rotation = Quaternion.Lerp(body.rotation, Quaternion.Euler(0, 0, bodyTilt), Time.deltaTime * leanSmoothing);

        // JUMP SQUASH / STRETCH
        float targetX = baseScale.x * (1 - vy * squashAmount);
        float targetY = baseScale.y * (1 + vy * stretchAmount);

        Vector3 targetScale = new Vector3(Mathf.Clamp(targetX, baseScale.x * 0.75f, baseScale.x * 1.2f), Mathf.Clamp(targetY, baseScale.y * 0.75f, baseScale.y * 1.2f), 1);

        whole.localScale = Vector3.Lerp(whole.localScale, targetScale, Time.deltaTime * squashSmooth);

        // MOVEMENT BOBBING
        if (speed > 0.1f)
        {
            float bob = Mathf.Sin(Time.time * bobSpeed) * bobAmount;
            whole.localPosition = new Vector3(0, bob, 0);
        }
        else
        {

            // IDLE BREATHING
            float t = Time.time;
            float breath = Mathf.Sin(t * idleBreathSpeed);

            // Small breathing scale
            Vector3 idleScale = baseScale + new Vector3(breath * idleBreathScale, breath * idleBreathScale * 0.7f, 0);

            whole.localScale = Vector3.Lerp(whole.localScale, idleScale, Time.deltaTime * 4f);

            // Gentle tilt on hands & body
            float idleTilt = breath * idleBreathTilt;

            hands.rotation = Quaternion.Lerp(hands.rotation, Quaternion.Euler(0, 0, idleTilt), Time.deltaTime * 3f);

            body.rotation = Quaternion.Lerp(body.rotation, Quaternion.Euler(0, 0, idleTilt * 0.6f), Time.deltaTime * 3f);

            // Slow bob (breathing-like)
            float idleBob = Mathf.Sin(t * idleBreathSpeed) * 0.02f;

            whole.localPosition = Vector3.Lerp(whole.localPosition, new Vector3(0, idleBob, 0), Time.deltaTime * 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Sprout")
        {
            handTiltMultiplier = 20;
            handNormal.SetActive(false);
            handSprout.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Pot")
        {
            if (handTiltMultiplier == 20)
            {
                if (!finishedLevel)
                {
                    CameraShakerHandler.Shake(bigShake);
                    playerCamera.DOMove(new Vector3 (playerCamera.position.x, playerCamera.position.y + 3, playerCamera.position.z), 4);
                    Expand();
                    finishedLevel = true;
                    playerController.canMove = false;
                }
            }
        }
    }

    public void Expand()
    {
        transformMask.DOScale(new Vector3(2500, 2500, 2500), 4);
        DOTween.To(() => greenVolume.weight, x => greenVolume.weight = x, 1f, 4);
        audio_Manager.playSound(audio_Manager.maskTransition);
        Invoke("FadeIn", 3);
        Invoke("FadeOut", 4);
        Invoke("SwitchEnvironment", 4);
    }

    private void FadeIn()
    {
        playerCamera.DOMove(new Vector3 (playerCamera.position.x, playerCamera.position.y, playerCamera.position.z), 15);
        fadeOverlay.DOFade(1, 1);
    }

    private void FadeOut()
    {
        fadeOverlay.DOFade(0, 1);
    }

    private void SwitchEnvironment()
    {
        transformMask.DOScale(new Vector3(0, 0, 0), 4f);
        environments[0].SetActive(false); // * This is the Grey Environment.
        environments[1].SetActive(true); // * This is the Green Environment.
        Invoke("TypingFinsihedLevel", 1);
    }

    private void TypingFinsihedLevel()
    {
        levelFinishedText.StartTyping();
        Invoke("FadeIn", 5);
    }
}
