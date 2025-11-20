using UnityEngine;
using System.Collections;


public class Audio_Manager : MonoBehaviour
{
    [SerializeField] AudioSource src;
    public AudioClip maskTransition;
    public AudioClip typing;
    public AudioClip jump;
    public AudioClip collect;
    public AudioClip rolling;
    public AudioClip damaging;
    public AudioClip healing;
    public AudioClip intro;
    public float pitch = 1;

    [SerializeField] AudioSource srcRolling;
    [SerializeField] AudioSource srcAura;

    private Coroutine auraFadeCoroutine;

    private void Start() 
    {
        srcAura.PlayOneShot(intro);
    }

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

    public void StartAura(AudioClip sfx)
    {
        // If new aura sound, update the clip
        if (srcAura.clip != sfx)
            srcAura.clip = sfx;

        srcAura.loop = true;

        // Start playing immediately but with volume 0
        if (!srcAura.isPlaying)
        {
            srcAura.volume = 0f;
            srcAura.Play();
        }

        // Stop any previous fade and start a new fade-in
        if (auraFadeCoroutine != null)
            StopCoroutine(auraFadeCoroutine);

        auraFadeCoroutine = StartCoroutine(FadeAura(1f)); // fade to full volume
    }

    public void StopAura()
    {
        if (!srcAura.isPlaying)
            return;

        // Stop any previous fade and start fade-out
        if (auraFadeCoroutine != null)
            StopCoroutine(auraFadeCoroutine);

        auraFadeCoroutine = StartCoroutine(FadeAura(0f)); // fade to zero volume
    }

    private IEnumerator FadeAura(float targetVolume)
    {
        float startVolume = srcAura.volume;
        float timer = 0f;

        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            float t = timer / 0.5f;

            srcAura.volume = Mathf.Lerp(startVolume, targetVolume, t);
            yield return null;
        }

        srcAura.volume = targetVolume;

        // After fading out, stop the audio
        if (targetVolume == 0f)
            srcAura.Stop();
    }

}
