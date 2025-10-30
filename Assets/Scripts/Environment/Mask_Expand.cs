using UnityEngine;
using DG.Tweening;

public class Mask_Expand : MonoBehaviour
{
    [SerializeField] RectTransform selfRect;
    [SerializeField] CanvasGroup maskAlphaObject;
    [SerializeField] GameObject[] environments;
    private Audio_Manager audio_Manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio_Manager = GameObject.Find("Audio Manager").GetComponent<Audio_Manager>();
        Invoke("Expand", 5);
    }

    public void Expand()
    {
        selfRect.DOSizeDelta(new Vector2(2500, 2500), 1);
        Invoke("SwitchEnvironment", 1);
        audio_Manager.playSound(audio_Manager.maskTransition);
    }

    private void SwitchEnvironment()
    {
        maskAlphaObject.DOFade(0, 1);
        environments[0].SetActive(false); // * This is the Grey Environment.
        environments[1].SetActive(true); // * This is the Green Environment.
    }
}
