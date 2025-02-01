using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingSound : MonoBehaviour
{
    public AudioSource audioSource;
    public TouchingDirections2 directions2;
    public PlayerController playerController;
    private bool wasGrounded;
    private bool isPlayingFootsteps = false;

    void Update()
    {
        if (!wasGrounded && directions2.IsGrounded)
        {
            PlayLandingSound();
        }
        HandleFootsteps();
        wasGrounded = directions2.IsGrounded;
    }

    void PlayLandingSound()
    {
        if (audioSource != null && playerController.fallSound != null)
        {
            audioSource.PlayOneShot(playerController.fallSound);
        }
    }

    void HandleFootsteps()
    {
        bool isMoving = playerController._isMoving;

        if (directions2.IsGrounded && isMoving)
        {
            if (!isPlayingFootsteps)
            {
                StartCoroutine(PlayFootstepsLoop());
            }
        }
        else
        {
            StopCoroutine(PlayFootstepsLoop());
            isPlayingFootsteps = false;
        }
    }

    IEnumerator PlayFootstepsLoop()
    {
        isPlayingFootsteps = true;
        while (directions2.IsGrounded && playerController._isMoving)
        {
            if (audioSource != null && playerController.footstepSound != null)
            {
                audioSource.PlayOneShot(playerController.footstepSound);
                yield return new WaitForSeconds(playerController.footstepSound.length); // Wait for the sound to finish
            }
            else
            {
                yield break;
            }
        }
        isPlayingFootsteps = false;
    }
}