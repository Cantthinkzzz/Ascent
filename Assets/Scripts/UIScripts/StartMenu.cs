using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StartMenu : MonoBehaviour
{
    private bool isFullscreen;
    public GameObject optionsMenu;
    public GameObject startMenu;
    public GameObject fullscreenButton;


    public void Start()
    {
        isFullscreen = Screen.fullScreen;
        if (isFullscreen)
        {
            TextMeshProUGUI buttonText = fullscreenButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "Windowed";
        }
        else {
            TextMeshProUGUI buttonText = fullscreenButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "Fullscreen";
        }
    }

    public void GameStart() {
        SceneManager.LoadScene("UI_Scene");
    }

    public void Options() {
        startMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void fullScreen() {
        if (!isFullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
            isFullscreen = true;
            TextMeshProUGUI buttonText = fullscreenButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "Windowed";
            Debug.Log("FULLSCRENE");
        }
        else {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
            isFullscreen = false;
            TextMeshProUGUI buttonText = fullscreenButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "Fullscreen";
            Debug.Log("WINDOWED");
        }
    }

    public void back() {
        startMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
    }
}
