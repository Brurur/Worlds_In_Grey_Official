using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbPlayer = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rbPlayer.linearVelocity.x);
    }
}
