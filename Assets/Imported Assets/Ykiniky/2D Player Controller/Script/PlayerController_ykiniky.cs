using System.Collections;
using UnityEngine;

namespace YkinikY
{
    public class PlayerController_ykiniky : MonoBehaviour
    {
        [Header("(c) Ykiniky")]
        [Header("Movement")]
        public bool canMove = true;
        public bool canJump = true;

        [Header("Movement Settings")]
        public float maxSpeed = 5f;
        public float acceleration = 12f;
        public float deceleration = 10f;

        private float currentSpeed = 0f;

        [Header("Camera")]
        public PlayerCameraFollow_ykiniky playerCameraFollow;
        public Vector2 lastCheckpoint;

        Rigidbody2D rb;

        // Flip (scale-based "paper" flip)
        private bool facingRight = true;
        private bool isFlipping = false;
        [Header("Flip Settings")]
        public float flipDuration = 0.18f;      // total time of the flip
        [Range(0.0f, 1.0f)] public float minWidth = 0.05f; // how thin during flip
        [Range(1.0f, 1.5f)] public float flipSquash = 1.1f; // vertical squash at thinnest point

        // store base scale so we don't overwrite other scales
        private Vector3 baseLocalScale;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            baseLocalScale = transform.localScale;
            // ensure facingRight matches initial localScale
            facingRight = baseLocalScale.x >= 0;
        }

        void Update()
        {
            if (canMove)
                HandleInput();

            HandleCameraFollow();
        }

        void HandleInput()
        {
            float input = Input.GetAxisRaw("Horizontal");

            // ACCELERATION / DECELERATION
            if (input != 0)
            {
                currentSpeed = Mathf.MoveTowards(
                    currentSpeed,
                    input * maxSpeed,
                    acceleration * Time.deltaTime
                );

                // Start flip coroutine instead of instantly changing scale
                if (input > 0 && !facingRight)
                    StartCoroutine(FlipScale(true)); // face right
                else if (input < 0 && facingRight)
                    StartCoroutine(FlipScale(false)); // face left
            }
            else
            {
                currentSpeed = Mathf.MoveTowards(
                    currentSpeed,
                    0f,
                    deceleration * Time.deltaTime
                );
            }

            // Jump
            if ((Input.GetKey(KeyCode.Space) && canJump) ||
                (Input.GetKey(KeyCode.W) && canJump))
            {
                canJump = false;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 6);
            }

            if (Input.GetButton("Jump") && canJump)
            {
                canJump = false;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 6);
            }
        }

        void FixedUpdate()
        {
            if (!canMove) return;

            // Apply horizontal movement USING RIGIDBODY2D
            rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
        }

        // Scale-based paper flip coroutine:
        // faceRight param = true -> target is facing right (positive x)
        private IEnumerator FlipScale(bool faceRight)
        {
            if (isFlipping) yield break;
            isFlipping = true;

            float half = flipDuration * 0.5f;
            float timer = 0f;

            // current scale values
            Vector3 startScale = transform.localScale;
            float startX = Mathf.Abs(startScale.x);
            float targetX = startX; // magnitude remains same, sign will change

            // first half: shrink X toward minWidth, squash Y toward flipSquash
            while (timer < half)
            {
                timer += Time.deltaTime;
                float t = timer / half;

                float width = Mathf.Lerp(startX, startX * minWidth, t);
                float height = Mathf.Lerp(startScale.y, baseLocalScale.y * flipSquash, t);

                transform.localScale = new Vector3(Mathf.Sign(startScale.x) * width, height, startScale.z);

                yield return null;
            }

            // ensure fully thin & squashed
            transform.localScale = new Vector3(Mathf.Sign(startScale.x) * (startX * minWidth), baseLocalScale.y * flipSquash, startScale.z);

            // swap facing (change sign)
            facingRight = faceRight;
            float newSign = faceRight ? 1f : -1f;

            // second half: expand X back to full width with new sign, return Y to base
            timer = 0f;
            while (timer < half)
            {
                timer += Time.deltaTime;
                float t = timer / half;

                float width = Mathf.Lerp(startX * minWidth, targetX, t);
                float height = Mathf.Lerp(baseLocalScale.y * flipSquash, baseLocalScale.y, t);

                transform.localScale = new Vector3(newSign * width, height, startScale.z);

                yield return null;
            }

            // restore exactly baseLocalScale but with correct sign on X
            transform.localScale = new Vector3(newSign * baseLocalScale.x, baseLocalScale.y, baseLocalScale.z);

            isFlipping = false;
        }

        void HandleCameraFollow()
        {
            if (playerCameraFollow == null) return;

            if (transform.position.x > 0)
                playerCameraFollow.FollowX();
            else
                playerCameraFollow.DontFollowX();

            if (transform.position.y > 0)
                playerCameraFollow.FollowY();
            else
                playerCameraFollow.DontFollowY();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            canJump = true;

            if (collision.gameObject.name == "PlayerSlower")
                BecomeSlow();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.name == "PlayerSlower")
                BecomeNormal();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.name == "Elevator")
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 6);

            if (collision.name == "Down_elevator")
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -4);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Nastro trasportatore s")
                rb.AddForce(new Vector2(-20, 0));

            if (collision.gameObject.name == "Nastro trasportatore d")
                rb.AddForce(new Vector2(20, 0));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Checkpoint")
                lastCheckpoint = transform.position;
        }

        public void TeleportPlayerX(float playerX)
        {
            transform.position = new Vector2(playerX, transform.position.y);
        }

        public void TeleportPlayerY(float playerY)
        {
            transform.position = new Vector2(transform.position.x, playerY);
        }

        public void BecomeSlow()
        {
            maxSpeed = 1.85f;
        }

        public void BecomeNormal()
        {
            maxSpeed = 5f;
        }

        // Allow animation script to read speed
        public float CurrentSpeed => currentSpeed;
    }
}
