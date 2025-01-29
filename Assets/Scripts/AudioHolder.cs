using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHolder : MonoBehaviour
{
    public AudioClip bellsAndCrows;
    private GameObject mainLik;
    private AudioSource audioSource;

    public void Start()
    {
        mainLik = GameObject.Find("main_lik");
        audioSource = mainLik.GetComponent<AudioSource>();
    }

    public void playBellsAndCrows()
    {
        if (audioSource != null && bellsAndCrows != null)
        {
            audioSource.PlayOneShot(bellsAndCrows);  // Play the sound
        }
    }
}

