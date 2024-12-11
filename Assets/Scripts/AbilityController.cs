using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour
{

    public Slider progresBar;
    public int essenceCount = 0;

    private int essenceRequired = 3; 
    private Movement movement; 

    void Start()
    {
        movement = FindAnyObjectByType<Movement>();
        progresBar.value = 0;
        progresBar.maxValue = essenceRequired;
    }

    void Update()
    {
        progresBar.value = essenceCount;

        if (essenceCount == 3 && !(movement.unlockedJumping))
        {
            Debug.Log("Otkljucao skakanje");
            movement.unlockedJumping = true;
            essenceCount = 0;
            essenceRequired = 5;
            progresBar.maxValue = essenceRequired;
            progresBar.value = 0;
        }

        else if (essenceCount == 5 && !(movement.unlockedDash) && movement.unlockedJumping)
        {
            Debug.Log("Otkljucao dash");
            movement.unlockedDash = true;
            essenceCount = 0;
            essenceRequired = 5;
            progresBar.maxValue = essenceRequired;
            progresBar.value = 0;
        }

        else if (essenceCount == 5 && !(movement.unlockedDoubleJump) && movement.unlockedJumping && movement.unlockedDash)
        {
            Debug.Log("Otkljucao dash");
            movement.unlockedDoubleJump = true;
            essenceCount = 0;
            essenceRequired = 3;
            progresBar.maxValue = essenceRequired;
            progresBar.value = 0;
        }

        else if (essenceCount == 3 && !(movement.unlockedWallJump) && movement.unlockedJumping && movement.unlockedDash && movement.unlockedDoubleJump)
        {
            Debug.Log("Otkljucao Wall Jump");
            movement.unlockedWallJump = true;
            essenceCount = 0;
            essenceRequired = 5;
            progresBar.maxValue = essenceRequired;
            progresBar.value = 0;
        }
    }
}
