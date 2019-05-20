using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool isGamePaused;
    public bool UIActive;
    public KeyCode key;
    public GameObject UIObject;

    void Awake()
    {
        UIActive = false;
        UIObject.SetActive(UIActive);
        isGamePaused = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            UIActive = (UIActive) ? false : true;
            UIObject.SetActive(UIActive);
            TogglePauseGame();
        }
    }

    /// <summary>
    /// On click method for Quit button 
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting game!");

        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        Application.Quit();
    }

    public void ResumeGame()
    {
        UIActive = false;
        UIObject.SetActive(UIActive);
        TogglePauseGame();
    }

    private void TogglePauseGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1;
            isGamePaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isGamePaused = true;
        }
    }
}
