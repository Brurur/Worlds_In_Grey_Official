using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    [SerializeField] AudioSource src;
    public AudioClip maskTransition;

    public void playSound(AudioClip sfx)
    {
        src.clip = sfx;
        src.volume = 0.3f;
        src.Play();
    }

}
