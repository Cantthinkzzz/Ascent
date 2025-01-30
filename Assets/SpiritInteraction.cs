using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpiritInteraction : MonoBehaviour
{
    public CanvasGroup fadePanel; // CanvasGroup for fading
    public TextMeshProUGUI questionText; // UI Text for the question
    public Button choice1Button; // Button for choice 1
    public Button choice2Button; // Button for choice 2
    public AudioClip fadeInSound; // Sound for screen fading in
    public AudioClip fadeOutSound; // Sound for screen fading out
    public AudioClip buttonClickSound; // Sound for button clicks
    public PlayerController player;
     public AudioSource bgm;
    private PlayerInput playerInput; 
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private EssenceText essenceText;
    private GameObject currentSpiritEssence; // The spirit essence being interacted with
    private float originalVolume;
    void Awake() {
        playerInput = player.GetComponentInParent<PlayerInput>();
        rb = player.GetComponentInParent<Rigidbody2D>();

    }
    private void Start()
    {
        GameObject bgmObject = GameObject.Find("BGM");
        if (bgmObject != null) {
            bgm = bgmObject.GetComponent<AudioSource>();
            originalVolume = bgm.volume;
        }
        fadePanel.alpha = 0;
        questionText.gameObject.SetActive(false);
        choice1Button.gameObject.SetActive(false);
        choice2Button.gameObject.SetActive(false);

        choice1Button.onClick.AddListener(() => OnChoiceSelected(1));
        choice2Button.onClick.AddListener(() => OnChoiceSelected(2));
    }

    public void TriggerSpiritEvent(GameObject spiritEssence)
    {
        currentSpiritEssence = spiritEssence;
        StartCoroutine(FadeToBlackAndShowQuestion());
    }
    private IEnumerator FadeAudio(float startVolume, float targetVolume, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            bgm.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        bgm.volume = targetVolume;
    }

    private IEnumerator FadeToBlackAndShowQuestion()
    {
       // PlaySound(fadeOutSound); // Play fade-out sound
        StartCoroutine(FadeAudio(bgm.volume, 0f, 1f));
        // Fade to black
        fadePanel.gameObject.SetActive(true);
        playerInput.enabled=false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        essenceText = currentSpiritEssence.GetComponent<EssenceText>();
        if(currentSpiritEssence.transform.childCount >0) {
            SpriteRenderer[] spriteRenderers = currentSpiritEssence.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            // Disable the SpriteRenderer
            renderer.enabled = false;
        }
        }
        else {
             spriteRenderer = currentSpiritEssence.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled=false;

        }
       
        while (fadePanel.alpha < 1)
        {
            fadePanel.alpha += Time.deltaTime;
            yield return null;
        }
        //questionText.text=essenceText.question;
        // Show the question and choices
        questionText.gameObject.SetActive(true);
        questionText.text=essenceText.question;
        //choice1Button.GetComponentInChildren<TextMeshProUGUI>().text= essenceText.option1;
        choice1Button.gameObject.SetActive(true);
        choice1Button.GetComponentInChildren<TextMeshProUGUI>().text= essenceText.option1;
        choice2Button.gameObject.SetActive(true);
        choice2Button.GetComponentInChildren<TextMeshProUGUI>().text= essenceText.option2;
    }

    private void OnChoiceSelected(int choice)
    {
        //PlaySound(buttonClickSound); // Play button click sound
        Debug.Log("Player chose: " + (choice == 1 ? "Yes" : "No"));
        if((choice==1&& essenceText.leftFight) || (choice==2 && !essenceText.leftFight)) player.FightOn++ ;
        else player.LiveOn++;
        // Hide the question and fade back in
        GameObject soundHolder = GameObject.Find("SoundHolder");
        AudioHolder audioHolder = soundHolder.GetComponent<AudioHolder>();
        if (audioHolder != null)
        {
            //audioHolder.playBellsAndCrows();
        }
        StartCoroutine(FadeToClear());
    }

    private IEnumerator FadeToClear()
    {
       // PlaySound(fadeInSound); // Play fade-in sound

        // Hide UI elements
        questionText.gameObject.SetActive(false);
        choice1Button.gameObject.SetActive(false);
        choice2Button.gameObject.SetActive(false);

        // Fade back to clear
        while (fadePanel.alpha > 0)
        {
            fadePanel.alpha -= Time.deltaTime;
            yield return null;
        }

        // Remove the spirit essence
        if (currentSpiritEssence != null)
        {
            Destroy(currentSpiritEssence);
        }
        playerInput.enabled=true;
    }

    /*private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    */
}

internal class AudioHolder
{
}