using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    [SerializeField] AudioSource src;
    public AudioClip maskTransition;
    public AudioClip typing;
    public AudioClip jump;
    public AudioClip collect;
    public AudioClip rolling;
    public float pitch = 1;

    [SerializeField] AudioSource srcRolling;

    public void playSound(AudioClip sfx)
    {
        src.pitch = pitch += pitch > 2 ? -1 : 0.2f;
        src.PlayOneShot(sfx);
    }

    public void stopSound()
    {
        src.pitch = 1;
        src.Stop();
    }

    public void StartRolling()
    {
        if (!srcRolling.isPlaying)
        {
            srcRolling.clip = rolling;
            srcRolling.loop = true;
            srcRolling.pitch = 1f;
            srcRolling.Play();
        }
    }

    public void StopRolling()
    {
        if (srcRolling.isPlaying)
            srcRolling.Stop();
    }

    public void SetRollingPitch(float speedPercent)
    {
        srcRolling.pitch = Mathf.Lerp(1f, 2f, speedPercent);
    }

}
