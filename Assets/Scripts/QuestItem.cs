using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D obj)
    {
        NPCInteract interaction = obj.GetComponent<NPCInteract>();
        if (obj.CompareTag("Player"))
        {
            interaction.nearSlavkoQuestItem = true;
        }
    }
    void OnTriggerExit2D(Collider2D obj)
    {
        NPCInteract interaction = obj.GetComponent<NPCInteract>();
        if (obj.CompareTag("Player"))
        {
            interaction.nearSlavkoQuestItem = false;
        }
    }
}
