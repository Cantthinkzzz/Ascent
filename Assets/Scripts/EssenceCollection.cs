using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceCollection : MonoBehaviour
{
    private AbilityController2 abilityController;
    public AudioSource audioSource;
    public PlayerController playerController;

    void Start()
    {
        abilityController = FindAnyObjectByType<AbilityController2>();
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Essence"))
        {
            if (abilityController != null)
            {
                abilityController.essenceCount++;
                if (audioSource != null && playerController.essencePickupSound != null)
                {
                    audioSource.PlayOneShot(playerController.essencePickupSound);
                }
            }

            Destroy(obj.gameObject);

            Debug.Log("Pokupijo jedan esenc");
        }
    }
}
