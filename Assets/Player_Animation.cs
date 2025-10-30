using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    [SerializeField] Transform playerHands, playerBody, playerWhole;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbPlayer = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHands.rotation = Quaternion.Euler(0, 0, rbPlayer.linearVelocity.x * -20);
        playerBody.rotation = Quaternion.Euler(0, 0, rbPlayer.linearVelocity.x * 4);
        playerWhole.localScale = new Vector3(Mathf.Clamp(1 - (rbPlayer.linearVelocity.y * 0.1f), 0.75f, 1), Mathf.Clamp(1 - (rbPlayer.linearVelocity.y * -0.05f), 1, 1.5f), 1);
    }
}
