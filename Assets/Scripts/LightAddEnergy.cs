using UnityEngine;

public class LightAddEnergy : MonoBehaviour
{
    private PlayerEnergy playerEnergy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerEnergy = GameObject.Find("Player").GetComponent<PlayerEnergy>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            playerEnergy.value = 1;
            playerEnergy.delay = 0.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            playerEnergy.value = -1;
            playerEnergy.delay = 1;
        }
    }
}
