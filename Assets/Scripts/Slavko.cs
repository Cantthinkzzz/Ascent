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
    public Animator animator;
    public bool Chilling {
         get{
            return animator.GetBool("relaxed");
        } set {
            animator.SetBool("relaxed", value);
        }
    }

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
