using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Death : MonoBehaviour
{
    public Animator playerAnimator;
    public ScreenFader screenFader;
    public PlayerInput playerInput;
    public Rigidbody2D rb;

    private LastSafePoint lastSafePoint;
    private bool isDead = false;

    private void Start()
    {
        GameObject lastSafePointObject = GameObject.Find("LastSafePoint");
        if (lastSafePointObject != null)
        {
            lastSafePoint = lastSafePointObject.GetComponent<LastSafePoint>();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hazard" && !isDead)
        {
            if (lastSafePoint != null)
            {
                StartCoroutine(HandleDeathAndRespawn(collision));
            }
        }
    }

    private IEnumerator HandleDeathAndRespawn(Collider2D collision)
    {
        isDead = true;

        if (playerInput != null)
        {
            playerInput.enabled = false;
        }

        if (rb != null)
        {
            rb.velocity = Vector2.zero; 
            rb.isKinematic = true;
        }

        playerAnimator.SetTrigger("Die");

        yield return screenFader.FadeOut();
        yield return new WaitForSeconds(0.5f);

        if (lastSafePoint != null)
        {
            transform.position = lastSafePoint.lastSafePoint;
        }

        if (rb != null)
        {
            rb.isKinematic = false; 
        }

        if (playerInput != null)
        {
            playerInput.enabled = true;
        }

        yield return screenFader.FadeIn();

        isDead = false;
    }
}