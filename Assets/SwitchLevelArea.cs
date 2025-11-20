using UnityEngine;

public class SwitchLevelArea : MonoBehaviour
{
    private Player_Animation playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<Player_Animation>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.SwitchLevelImmediately();
        }
    }
}
