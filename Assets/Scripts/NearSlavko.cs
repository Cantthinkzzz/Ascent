using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearSlavko : MonoBehaviour
{ 
    void OnTriggerEnter2D(Collider2D obj)
    {
        NPCInteract interaction = obj.GetComponent<NPCInteract>();
        if (obj.CompareTag("Player"))
        {
            interaction.nearSlavko = true;
        }
    }
    void OnTriggerExit2D(Collider2D obj)
    {
        NPCInteract interaction = obj.GetComponent<NPCInteract>();
        if (obj.CompareTag("Player"))
        {
            interaction.nearSlavko = false;
        }
    }
}
