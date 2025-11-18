using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

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

    IEnumerator TypeAll()
    {
        foreach (var sentence in sentences)
        {
            textField.text = "";

            foreach (char c in sentence)
            {
                textField.text += c;
                yield return new WaitForSeconds(letterDelay);
            }

            yield return new WaitForSeconds(sentenceDelay);
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
}
