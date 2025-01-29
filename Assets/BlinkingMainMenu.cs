using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingMainMenu : MonoBehaviour
{
    public Sprite openEye; 
    public Sprite closedEye; 
    public Image picture; 

    private Coroutine blinkCoroutine;
    private void OnEnable()
    {
        if (picture == null)
        {
            picture = GetComponent<Image>();
        }
        blinkCoroutine = StartCoroutine(Blink());
    }

    private void OnDisable()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            picture.sprite = openEye;
            float openDuration = Random.Range(1.5f, 2f);
            yield return new WaitForSeconds(openDuration);

            picture.sprite = closedEye;
            float closedDuration = Random.Range(0.1f, 0.4f);
            yield return new WaitForSeconds(closedDuration);
        }
    }
}
