using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    [Header("Light Renderers")]
    public Renderer[] lightRenderers = new Renderer[3];

    [Header("Light Colors")]
    public Color onColor = Color.yellow;
    public Color offColor = Color.gray;

    [Header("Events")]
    public UnityEvent onPuzzleCompleted;

    private bool[] lightStates = new bool[6];
    public bool puzzleSolved = false;

    public void PressButtonA() => ToggleLights(0, 3, 4);
    public void PressButtonB() => ToggleLights(1, 4, 5);
    public void PressButtonC() => ToggleLights(0, 2, 3);
    public void PressButtonD() => ToggleLights(1, 2, 5);

    void Start()
    {
        lightStates[2] = !lightStates[2];
        lightRenderers[2].material.color = lightStates[2] ? onColor : offColor;
        lightStates[4] = !lightStates[4];
        lightRenderers[4].material.color = lightStates[4] ? onColor : offColor;
    }
    private void ToggleLights(int index1, int index2, int index3)
    {
        ToggleLight(index1);
        ToggleLight(index2);
        ToggleLight(index3);
        CheckPuzzleSolved();
    }

    private void ToggleLight(int index)
    {
        lightStates[index] = !lightStates[index];
        lightRenderers[index].material.color = lightStates[index] ? onColor : offColor;
    }

    private void CheckPuzzleSolved()
    {
        if (!puzzleSolved && lightStates[0] && lightStates[1] && lightStates[2] && lightStates[3] && lightStates[4] && lightStates[5])
        {
            puzzleSolved = true;
            onPuzzleCompleted.Invoke();
            Debug.Log("Yay");
        }
    }
}
