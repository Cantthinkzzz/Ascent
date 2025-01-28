using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slavko : MonoBehaviour
{

    public bool hasMetPlayer = false;
    public Sprite idle;
    public Sprite panicked;
    public Sprite calm;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            if (!hasMetPlayer) {
                spriteRenderer.sprite = panicked;
            }
            else{
               spriteRenderer.sprite = calm;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D obj) 
    {
        spriteRenderer.sprite = idle;
    }
}
