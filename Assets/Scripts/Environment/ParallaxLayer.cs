using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxStrength = 0.1f;

    private Transform cam;
    private Vector3 lastCamPosition;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPosition = cam.position;
    }

    void LateUpdate()
    {
        Vector3 camMovement = cam.position - lastCamPosition;
        transform.position += new Vector3(camMovement.x, camMovement.y, 0) * parallaxStrength;
        lastCamPosition = cam.position;
    }
}
