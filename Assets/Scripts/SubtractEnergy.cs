using UnityEngine;
using YkinikY;

public class SubtractEnergy : MonoBehaviour
{
    private PlayerEnergy playerEnergy;
    private PlayerController_ykiniky playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerEnergy = GameObject.Find("Player").GetComponent<PlayerEnergy>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController_ykiniky>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            playerEnergy.value = -1;
            playerEnergy.delay = 0.05f;
            playerController.maxSpeed = 2;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            playerEnergy.value = -1;
            playerEnergy.delay = 1;
            playerController.maxSpeed = 4;
        }
    }
}
