using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    private Transform platformTransform;
    [SerializeField] Vector3 targetPos = new Vector3(0, 0, 0);
    [SerializeField] float targetSpeed = 0;

    void Start()
    {
        platformTransform = gameObject.GetComponent<Transform>();
        platformTransform.DOMove(targetPos, targetSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }
}
