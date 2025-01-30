using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearBox : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D obj)
    {
        NPCInteract interaction = obj.GetComponent<NPCInteract>();
        if (obj.CompareTag("Player"))
        {
            interaction.nearBox = true;
            interaction.box = gameObject.transform;
        }
    }
    void OnTriggerExit2D(Collider2D obj)
    {
        NPCInteract interaction = obj.GetComponent<NPCInteract>();
        if (obj.CompareTag("Player"))
        {
            interaction.nearBox = false;
            interaction.box = null;
        }
    }
}
