using UnityEngine;
using DG.Tweening;

public class Sprout : MonoBehaviour
{
    [SerializeField] GameObject sprout;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprout.transform.DORotate(new Vector3(0, 180, 0), 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        sprout.transform.DOScale(new Vector3(sprout.transform.localScale.x * 0.8f, sprout.transform.localScale.y * 0.8f, sprout.transform.localScale.z * 0.8f), 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
