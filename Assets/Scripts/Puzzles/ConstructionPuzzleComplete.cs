using UnityEngine;
using DG.Tweening;

public class ConstructionPuzzleComplete : MonoBehaviour
{
    [SerializeField] MovingPlatform[] platforms;

    private Transform cameraTransform;
    [SerializeField] Vector3 targetPos = new Vector3(0, 0, 0);
    [SerializeField] float targetSpeed = 0;

    private Audio_Manager audio_Manager;

    private void Start() 
    {
        audio_Manager = GameObject.Find("Audio Manager").GetComponent<Audio_Manager>();
        cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    public void StartPlatforms()
    {
        audio_Manager.playSound(audio_Manager.collect);
        cameraTransform.DOMove(targetPos, targetSpeed).SetEase(Ease.InOutQuad);

        foreach (var item in platforms)
        {
            item.enabled = true;
        }
    }
}
