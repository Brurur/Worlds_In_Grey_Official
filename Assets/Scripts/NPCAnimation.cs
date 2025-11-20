using UnityEngine;
using DG.Tweening;

public class NPCAnimation : MonoBehaviour
{
    [Header("Breathing Settings")]
    [SerializeField] float scaleAmount = 0.05f;   // how much the chest expands
    [SerializeField] float bobAmount = 0.03f;     // slight vertical movement
    [SerializeField] float breathTime = 1.5f;     // full inhale-exhale time

    private Vector3 baseScale;
    private Vector3 basePos;

    void Start()
    {
        baseScale = transform.localScale;
        basePos = transform.localPosition;

        StartBreathing();
    }

    void StartBreathing()
    {
        // Scale breathing (inhale → exhale)
        transform.DOScale(baseScale + new Vector3(scaleAmount, scaleAmount * 0.7f, 0), breathTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

        // Vertical bobbing
        transform.DOLocalMoveY(basePos.y + bobAmount, breathTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
