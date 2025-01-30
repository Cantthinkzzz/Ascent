using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class cutscene : MonoBehaviour
{
    public Image displayImage; // UI Image component to display the pictures
    public Sprite[] pictures; // Array of images to switch between
    public float switchTime = 5f; // Time interval for switching images
    public string sceneToLoad; // Name of the scene to load after the last image

    private int currentIndex = 0;

    void Start()
    {
        if (pictures.Length > 0)
        {
            displayImage.sprite = pictures[currentIndex];
            StartCoroutine(SwitchImages());
        }
        else
        {
            Debug.LogError("No images assigned to the ImageSwitcher!");
        }
    }

    IEnumerator SwitchImages()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchTime);
            currentIndex++;

            if (currentIndex >= pictures.Length)
            {
                SceneManager.LoadScene(sceneToLoad);
                yield break;
            }

            displayImage.sprite = pictures[currentIndex];
        }
    }
}
