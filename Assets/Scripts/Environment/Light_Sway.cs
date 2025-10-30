using UnityEngine;
using DG.Tweening;

public class Light_Sway : MonoBehaviour
{
    private RectTransform selfRect;
    private Vector3 currentPosition, targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selfRect = gameObject.GetComponent<RectTransform>();
        currentPosition = selfRect.position;
        targetPosition = new Vector3(currentPosition.x + 1, currentPosition.y + 1, currentPosition.z + 1);
        Sway();
    }

    private void Sway()
    {
        selfRect.DOMove(targetPosition, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
