using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefconZ.UI
{
    /// <summary>
    /// Stores toggles and pause settings.
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        public bool isGamePaused;
        public bool UIActive;
        public KeyCode key;
        public GameObject UIObject;

        [SerializeField]
        private SettingsMenu settingsMenu;
        [SerializeField]
        private GameObject settingsMenuObject;

        void Awake()
        {
            SceneManager.LoadScene("SettingsMenu", LoadSceneMode.Additive);
            UIActive = false;
            UIObject.SetActive(UIActive);
            isGamePaused = false;
        }

        private void Start()
        {
            settingsMenuObject = GameObject.Find("SettingsMenuCanvas");
            settingsMenu = settingsMenuObject.GetComponent<SettingsMenu>();
        }

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
        /// Settings menu button toggle 
        /// </summary>
        public void SettingsMenuButton()
        {
            settingsMenu.ToggleActive(this.gameObject);
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
        /// <summary>
        /// On click method resuming the game.
        /// </summary>
        public void ResumeGame()
        {
            UIActive = false;
            UIObject.SetActive(UIActive);
            TogglePauseGame();
        }
        /// <summary>
        /// Pause menu toggle set via timescale.
        /// </summary>
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
}