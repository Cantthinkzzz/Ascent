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
    public bool triggersNPCEvent=false;
    public Transform box;
    public Transform essenceLocation;
    public GameObject essence;
    public PlayerController playerController;
    public AudioSource audioSource;
    public SpriteRenderer eyes;

    public void Interact()
    {
            
        if (nearSlavko) {
                
            if (!slavko.hasMetPlayer) {
                    slavko.hasMetPlayer = true;
                    slavko.spriteRenderer.sprite = slavko.calm;
                    if (audioSource != null && playerController.slavkoSpeakSound != null)
                {
                    audioSource.PlayOneShot(playerController.slavkoSpeakSound);
                }         
            }
                
            if (!slavkoQuestCompleted){
                if (audioSource != null && playerController.slavkoSpeakSound != null)
                {
                    audioSource.PlayOneShot(playerController.slavkoSpeakSound);
                }
                    slavkoQuest.SetActive(true);
                    slavkoQuestItem.SetActive(true);
                    StartCoroutine(RemoveIcon(slavkoQuest));
            }
                
            else {
                    //Spawnaj jedan esenc ili nesto idk
                    essence.transform.position= essenceLocation.position;
                    slavko.Chilling=true;
                    slavkoQuestComplete.SetActive(true);
                    if(eyes!= null) {
                        eyes.enabled=true;
                    }
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
            //rb2D.constraints= RigidbodyConstraints2D.None;
            rb2D.isKinematic=false;
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
            if (audioSource != null && playerController.boxPushSound != null)
            {
                audioSource.PlayOneShot(playerController.boxPushSound);
            }
            
        }
    }

    public IEnumerator RemoveIcon(GameObject npcQuest) {
        yield return new WaitForSeconds(5);
        npcQuest.SetActive(false);
    }
}
