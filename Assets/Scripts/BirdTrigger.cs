using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            PlayerController movement = obj.GetComponent<PlayerController>();
            if (movement.unlockedJumping)
            {
                Debug.Log("PUSTI ZVUK");
                audioSource.PlayOneShot(audioClip);
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
