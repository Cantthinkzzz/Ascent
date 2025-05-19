using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private SpriteRenderer border, background;
    private TextMeshPro textRenderer;
    public float appearTime = 0.25f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) {
            StopCoroutine(Disappear());
            StartCoroutine(Appear());
        }
       
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) {
            StopCoroutine(Appear());
            StartCoroutine(Disappear());
        }
       
    }
    void Awake()
    {
        border= gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        background= gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        textRenderer = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();
        border.color = new Color(border.color.r, border.color.g, border.color.b, 0);
            background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
            textRenderer.alpha = 0;

    }
    public IEnumerator Appear() {
        float timer = textRenderer.alpha /appearTime;
        while(timer <appearTime) {
            timer =Mathf.Min(appearTime, timer+Time.deltaTime);
            float alpha = timer / appearTime;
            border.color = new Color(border.color.r, border.color.g, border.color.b, alpha);
            background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
            textRenderer.alpha = alpha;
            yield return null;
        }
    }
    public IEnumerator Disappear() {
        float timer = textRenderer.alpha /appearTime;
        while(timer >0f) {
            timer =Mathf.Max(0f, timer-Time.deltaTime);
            float alpha = timer / appearTime;
            border.color = new Color(border.color.r, border.color.g, border.color.b, alpha);
            background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
            textRenderer.alpha = alpha;
            //timer =Mathf.Max(0f, timer-Time.deltaTime);
            yield return null;
        }
    }
}
