using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController2 : MonoBehaviour
{

    public Slider progresBar;
    public int essenceCount = 0;

    private int essenceRequired = 3; 
    private PlayerController movement;

    public ScreenFader screenFader;
    public GameObject abilityUnlockUI;
    public Image abilityImage;
    public TextMeshProUGUI abilityNameText;
    public TextMeshProUGUI abilityDescriptionText;
    public AudioSource audioSource;
    public AudioClip abilityUnlockSound;

    public Sprite jumpIcon, dashIcon, doubleJumpIcon, wallJumpIcon, hazardRemoverIcon;

    void Start()
    {
        movement = FindAnyObjectByType<PlayerController>();
        progresBar.value = 0;
        progresBar.maxValue = essenceRequired;
    }

    void Update()
    {
        progresBar.value = essenceCount;

        if (essenceCount == 3 && !(movement.unlockedJumping))
        {
            Debug.Log("Otkljucao skakanje");
            UnlockAbility("Jump unlocked", "Press Space to jump", jumpIcon);
            movement.unlockedJumping = true;
            essenceCount = 0;
            essenceRequired = 5;
            progresBar.maxValue = essenceRequired;
            progresBar.value = 0;
        }

        else if (essenceCount == 5 && !(movement.unlockedDash) && movement.unlockedJumping)
        {
            Debug.Log("Otkljucao dash");
            UnlockAbility("Dash unlocked", "Press Left Click to dash", dashIcon);
            movement.unlockedDash = true;
            essenceCount = 0;
            essenceRequired = 5;
            progresBar.maxValue = essenceRequired;
            progresBar.value = 0;
        }

        else if (essenceCount == 5 && !(movement.unlockedDoubleJump) && movement.unlockedJumping && movement.unlockedDash)
        {
            Debug.Log("Otkljucao dash");
            UnlockAbility("Double Jump unlocked", "Press Space after jumping for a second jump", doubleJumpIcon);
            movement.unlockedDoubleJump = true;
            essenceCount = 0;
            essenceRequired = 3;
            progresBar.maxValue = essenceRequired;
            progresBar.value = 0;
        }

        else if (essenceCount == 3 && !(movement.unlockedWallJump) && movement.unlockedJumping && movement.unlockedDash && movement.unlockedDoubleJump)
        {
            Debug.Log("Otkljucao Wall Jump");
            UnlockAbility("Wall Jump unlocked", "Jump towards a wall to cling and press Space to jump off", wallJumpIcon);
            movement.unlockedWallJump = true;
            essenceCount = 0;
            essenceRequired = 5;
            progresBar.maxValue = essenceRequired;
            progresBar.value = 0;
        }
    }

    void UnlockAbility(string abilityName, string abilityDescription, Sprite abilityIcon)
    {
        StartCoroutine(ShowAbilityUnlockScreen(abilityIcon, abilityName, abilityDescription));
    }

    IEnumerator ShowAbilityUnlockScreen(Sprite abilityIcon, string abilityName, string abilityDescription)
    {
        yield return screenFader.FadeOut(); // Fade to black

        abilityUnlockUI.SetActive(true);
        abilityImage.sprite = abilityIcon;
        abilityNameText.text = abilityName;
        abilityDescriptionText.text = abilityDescription;

        if (audioSource != null && abilityUnlockSound != null)
        {
            audioSource.PlayOneShot(abilityUnlockSound);
        }

        yield return new WaitForSeconds(3f); // Wait before fading back

        abilityUnlockUI.SetActive(false);
        yield return screenFader.FadeIn(); // Fade back to normal
    }
}
