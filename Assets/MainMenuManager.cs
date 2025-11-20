using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    private CanvasGroup fadeOverlay;

    private void Start() 
    {
        Cursor.visible = true;
        fadeOverlay = GameObject.Find("Fade Overlay").GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        fadeOverlay.DOFade(1, 1);
    }
    
    public void StartGame()
    {
        Invoke("SwitchToStart", 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void SwitchToStart()
    {
        SceneManager.LoadScene("Level 1");
    }
}
