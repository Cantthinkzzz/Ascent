using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject MenuPause;
    public GameObject OptionsMenu;
    public GameObject playerInput;
    private bool isPaused = false;
    public InputActionMap playerControls;

    void Awake()
    {
        MenuPause.gameObject.SetActive(false);
        var input = playerInput.GetComponent<PlayerInput>().actions;
        playerControls = input.FindActionMap("Player");
        Debug.Log("STARTT");
    }

    public void Pauziraj()
    {
        Debug.Log("POZVAO PAUSE");
        if (isPaused)
        {
            Time.timeScale = 1.0f;
            MenuPause.gameObject.SetActive(false);
            AudioListener.pause = false;
            playerControls.Enable();
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            MenuPause.gameObject.SetActive(true);
            AudioListener.pause = true;
            playerControls.Disable();
            isPaused = true;

        }
    }

    public void StartMenu()
    {
        Debug.Log("into start");
        isPaused = true;
        Time.timeScale = 1.0f;

        MenuPause.gameObject.SetActive(false);
        AudioListener.pause = false;
        playerControls.Enable();
        Debug.Log("About to load");
        SceneManager.LoadScene("Start_Menu");
        Debug.Log("Loaded");

    }

    public void Options()
    {
        MenuPause.gameObject.SetActive(false);
        OptionsMenu.gameObject.SetActive(true);

    }

    public void Back()
    {
        MenuPause.gameObject.SetActive(true);
        OptionsMenu.gameObject.SetActive(false);
    }
}
