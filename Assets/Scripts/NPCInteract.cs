using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCInteract : MonoBehaviour
{
    public bool nearSlavko = false;
    public bool slavkoQuestCompleted = false;
    public bool nearSlavkoQuestItem = false;
    public Slavko slavko;
    public GameObject slavkoQuest;
    public GameObject slavkoQuestComplete;
    public GameObject slavkoQuestItem;
    public bool nearBox = false;
    public Transform box;

    public void Interact()
    {
            
        if (nearSlavko) {
                
            if (!slavko.hasMetPlayer) {
                    slavko.hasMetPlayer = true;
                    slavko.spriteRenderer.sprite = slavko.calm;         
            }
                
            if (!slavkoQuestCompleted){
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

        if (nearBox) {
            box.transform.position += (Vector3)(Vector2.left) * 10f; //Ili right ovisno u koju stranu treba pomaknuti
        }
    }

    public IEnumerator RemoveIcon(GameObject npcQuest) {
        yield return new WaitForSeconds(5);
        npcQuest.SetActive(false);
    }
}
