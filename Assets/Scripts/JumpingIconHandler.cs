using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingIconHandler : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject unlockedJumpingIcon;

    private bool isJumpingIconUsed = false;

    void Start()
    {
        unlockedJumpingIcon.SetActive(false);
    }

    void Update()
    {
        if (playerController.unlockedJumping)
        {
            unlockedJumpingIcon.SetActive(true);

            if (playerController.isJumping && !isJumpingIconUsed)
            {
                unlockedJumpingIcon.transform.Find("NotUsedJump").gameObject.SetActive(false);
                unlockedJumpingIcon.transform.Find("UsedJump").gameObject.SetActive(true);
                isJumpingIconUsed = true;
                Debug.Log("Jumping icon set to Used");
            }
            else if (!playerController.isJumping && isJumpingIconUsed)
            {
                unlockedJumpingIcon.transform.Find("NotUsedJump").gameObject.SetActive(true);
                unlockedJumpingIcon.transform.Find("UsedJump").gameObject.SetActive(false);
                isJumpingIconUsed = false;
                Debug.Log("Jumping icon set to NotUsed");
            }
        }
        else
        {
            unlockedJumpingIcon.SetActive(false);
        }
    }
}

