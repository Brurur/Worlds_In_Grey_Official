using UnityEngine;
using DG.Tweening;

public class Particle_Birds : MonoBehaviour
{
    [SerializeField] Sprite[] birds;
    [SerializeField] Vector3 targetPosition;
    private Vector3 currentPosition;
    [SerializeField] float duration;
    [SerializeField] float delay;
    private RectTransform selfRect;

    private void Start()
    {
        selfRect = gameObject.GetComponent<RectTransform>();
        currentPosition = selfRect.position;
        BirdOne();
        Move();
    }

    private void BirdOne()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = birds[0];
        Invoke("BirdTwo", 0.25f);
    }

    private void BirdTwo()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = birds[1];
        Invoke("BirdOne", 0.25f);
    }

    private void Move()
    {
        selfRect.position = currentPosition;
        selfRect.DOMove(targetPosition, duration).SetEase(Ease.InSine);
        Invoke("Move", delay);
    }
}
