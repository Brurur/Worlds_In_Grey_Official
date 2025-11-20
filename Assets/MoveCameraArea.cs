using UnityEngine;
using DG.Tweening;
using YkinikY;

public class MoveCameraArea : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] Vector3 targetPos = new Vector3(0, 0, 0);
    [SerializeField] float targetSpeed = 0;
    [SerializeField] bool enableMovement = false;
    private bool hasBeenPlayed = false;
    private PlayerController_ykiniky playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController_ykiniky>();
        cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player" && !hasBeenPlayed)
        {
            if (!enableMovement)
            {
                playerController.canMove = false;
                Invoke("TurnOnMovement", targetSpeed);
            }

            hasBeenPlayed = true;
            cameraTransform.DOMove(targetPos, targetSpeed).SetEase(Ease.InOutQuad);
        }    
    }

    private void TurnOnMovement()
    {
        playerController.canMove = true;
    }
}
