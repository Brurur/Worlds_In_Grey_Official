using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI energyText;
    public int energy = 100;
    public float delay = 1;
    public int value = -1;

    private CanvasGroup fadeOverlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadeOverlay = GameObject.Find("Fade Overlay").GetComponent<CanvasGroup>();
        fadeOverlay.DOFade(1, 0.01f);
        Invoke("FadeIn", 1);
        
        ChangeEnergy();    
    }

    private void FadeIn()
    {
        fadeOverlay.DOFade(0, 2);
    }

    public void ChangeEnergy()
    {
        energy += value;
        energy = Mathf.Clamp(energy, 0, 100);
        energyText.text = energy.ToString() + "%";
        Invoke("ChangeEnergy", delay);

        if (energy == 0)
        {
            fadeOverlay.DOFade(1, 1);
            Invoke("RestartScene", 2);
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
