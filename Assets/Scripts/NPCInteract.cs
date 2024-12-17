using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public bool nearSlavko = false;
    public bool slavkoQuestCompleted = false;
    public bool nearSlavkoQuestItem = false;
    public Slavko slavko;
    public GameObject slavkoQuest;
    public GameObject slavkoQuestComplete;
    public GameObject slavkoQuestItem;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (nearSlavko) {
                if (!slavko.hasMetPlayer) {
                    slavko.hasMetPlayer = true;
                    slavko.spriteRenderer.sprite = slavko.calm;
                }
                if (!slavkoQuestCompleted)
                {
                    slavkoQuest.SetActive(true);
                    slavkoQuestItem.SetActive(true);
                    StartCoroutine(RemoveIcon(slavkoQuest));
                }
                else {
                    //Spawnaj jedan esenc ili nesto idk
                    slavkoQuestComplete.SetActive(true);
                    StartCoroutine(RemoveIcon(slavkoQuestComplete));
                }
            }
            if (nearSlavkoQuestItem) {
                slavkoQuestCompleted = true;
                Destroy(slavkoQuestItem);
            }
        }
    }

    public IEnumerator RemoveIcon(GameObject npcQuest) {
        yield return new WaitForSeconds(5);
        npcQuest.SetActive(false);
    }
}
