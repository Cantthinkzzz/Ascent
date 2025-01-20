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
            Vector2 boxPosition = box.transform.position;
            Bounds boxBounds = box.GetComponent<BoxCollider2D>().bounds;
            Rigidbody2D rb2D = box.GetComponent<Rigidbody2D>();
            Debug.Log(rb2D);
            if (boxPosition.x < transform.position.x)
            {
                //box.transform.position -= (Vector3)(Vector2.right) * 10f; //Ili right ovisno u koju stranu treba pomaknuti


                rb2D.velocity = new Vector2(-50f, rb2D.velocity.x);
                Debug.Log("lijevo " + rb2D.velocity);
            }
            else
            {
                //box.transform.position += (Vector3)(Vector2.right) * 10f; //Ili right ovisno u koju stranu treba pomaknuti
                Debug.Log("desno");
                rb2D.velocity = new Vector2(50f, rb2D.velocity.y);
            }
            
        }
    }

    public IEnumerator RemoveIcon(GameObject npcQuest) {
        yield return new WaitForSeconds(5);
        npcQuest.SetActive(false);
    }
}
