using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpIconHandler : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject unlockedWallJumpIcon;

    private bool isWallJumpingIconUsed = false;

    void Start()
    {
        unlockedWallJumpIcon.SetActive(false);
    }

    void Update()
    {
        if (playerController.unlockedWallJump)
        {
            unlockedWallJumpIcon.SetActive(true);

            if (playerController.isWallJumping && !isWallJumpingIconUsed)
            {
                unlockedWallJumpIcon.transform.Find("NotUsedWall").gameObject.SetActive(false);
                unlockedWallJumpIcon.transform.Find("UsedWall").gameObject.SetActive(true);
                isWallJumpingIconUsed = true;
            }
            else if (!playerController.isWallJumping && isWallJumpingIconUsed)
            {
                unlockedWallJumpIcon.transform.Find("NotUsedWall").gameObject.SetActive(true);
                unlockedWallJumpIcon.transform.Find("UsedWall").gameObject.SetActive(false);
                isWallJumpingIconUsed = false;
            }
        }
        else
        {
            unlockedWallJumpIcon.SetActive(false);
        }
    }
}
