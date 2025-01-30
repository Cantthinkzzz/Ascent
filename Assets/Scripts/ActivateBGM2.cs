using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBGM2 : MonoBehaviour
{
    public AudioSource song1; // First song to activate
    public AudioSource song2; // Second song to deactivate and reset


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(HandleMusicTransition());
        }
    }

    private IEnumerator HandleMusicTransition()
    {
        if (song2 != null && song2.isPlaying)
        {
            Debug.Log("Fading out song2");
            StartCoroutine(FadeOut(song2, 2f)); // Fade out song2 over 2 seconds
        }

        // Wait for 5 seconds before proceeding to the next action
        yield return new WaitForSeconds(5f);

        if (song1 != null && !song1.isPlaying)
        {
            Debug.Log("Fading in song1");
            StartCoroutine(FadeIn(song1, 2f)); // Fade in song1 over 2 seconds
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;
        Debug.Log("Starting FadeOut");

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Restore volume
        Debug.Log("FadeOut complete");
    }

    IEnumerator FadeIn(AudioSource audioSource, float fadeDuration)
    {
        audioSource.Play();
        audioSource.volume = 0;
        Debug.Log("Starting FadeIn");

        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        Debug.Log("FadeIn complete");
    }
}