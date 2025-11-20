using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    public enum ButtonType { A, B, C, D}
    public ButtonType buttonType;
    public PuzzleManager puzzleManager;

    private SpriteRenderer sr;
    private Audio_Manager audio_Manager;

    private void Start() 
    {
        sr = GetComponent<SpriteRenderer>();    
        audio_Manager = GameObject.Find("Audio Manager").GetComponent<Audio_Manager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !puzzleManager.puzzleSolved)
        {
            sr.flipY = sr.flipY ? false : true;
            audio_Manager.playSound(audio_Manager.typing);
        switch (buttonType)
        {
            case ButtonType.A:
                puzzleManager.PressButtonA();
                break;
            case ButtonType.B:
                puzzleManager.PressButtonB();
                break;
            case ButtonType.C:
                puzzleManager.PressButtonC();
                break;
            case ButtonType.D:
                puzzleManager.PressButtonD();
                break;
        }
        }
    }
}
