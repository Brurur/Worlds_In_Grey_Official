using UnityEngine;
using TMPro;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI energyText;
    public int energy = 100;
    public float delay = 1;
    public int value = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeEnergy();    
    }

    public void ChangeEnergy()
    {
        energy += value;
        energy = Mathf.Clamp(energy, 0, 100);
        energyText.text = energy.ToString() + "%";
        Invoke("ChangeEnergy", delay);
    }
}
