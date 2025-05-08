using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public PlayerController playerController;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("load");
        if(other.CompareTag("Player")) {
            if(playerController.LiveOn + playerController.FightOn < 8){
                SceneManager.LoadScene("Bad");
            }
            else if(playerController.LiveOn <= playerController.FightOn) {
                SceneManager.LoadScene("True");
            }
            else SceneManager.LoadScene("Mid");
        }
    }
}
