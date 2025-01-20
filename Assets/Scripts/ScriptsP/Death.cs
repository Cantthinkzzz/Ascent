using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Animator playerAnimator;
    public ScreenFader screenFader;

    private LastSafePoint lastSafePoint;

    private void Start()
    {
        GameObject lastSafePoinObject = GameObject.Find("LastSafePoint");
        if (lastSafePoinObject != null) {
            lastSafePoint = lastSafePoinObject.GetComponent<LastSafePoint>();
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hazard") {
            if (lastSafePoint != null)
            {
                StartCoroutine(HandleDeathAndRespawn());
            }
        }
    }

    private IEnumerator HandleDeathAndRespawn()
    {
        playerAnimator.SetTrigger("Die");
        yield return screenFader.FadeOut();
        yield return new WaitForSeconds(0.5f);

        if (lastSafePoint != null)
        {
            transform.position = lastSafePoint.lastSafePoint;
        }
        yield return screenFader.FadeIn();
    }
}
