using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using YkinikY;

public class TypeWriter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshPro textField;

    [Header("Sentences")]
    [TextArea(2, 4)]
    [SerializeField] List<string> sentences;

    [Header("Timing")]
    [SerializeField] float letterDelay = 0.04f;
    [SerializeField] float sentenceDelay = 1f;

    private bool hasBeenPlayed = false;
    private Audio_Manager audio_Manager;
    private PlayerController_ykiniky playerController;
    public bool changeMovement = true;

    private void Start() 
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController_ykiniky>();
        audio_Manager = GameObject.Find("Audio Manager").GetComponent<Audio_Manager>();
    }

    IEnumerator TypeAll()
    {
        if (changeMovement)
        {
            playerController.canMove = false;
        }
        
        foreach (var sentence in sentences)
        {
            textField.text = "";

            foreach (char c in sentence)
            {
                audio_Manager.playSound(audio_Manager.typing);
                textField.text += c;
                yield return new WaitForSeconds(letterDelay);
            }

            yield return new WaitForSeconds(sentenceDelay);
        }

        if (changeMovement)
        {
            playerController.canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!hasBeenPlayed)
        {
            hasBeenPlayed = true;
            
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(TypeAll());
            }
        } 
    }

    public void StartTyping()
    {
        changeMovement = false;
        hasBeenPlayed = true;
        StartCoroutine(TypeAll());
    }
}
