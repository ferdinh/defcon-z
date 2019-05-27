using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefconZ.UI
{
    /// <summary>
    /// Holds variables for the main menu, and provides onclick methods for menu buttons
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        public GameObject menuPanel;
        public SettingsMenu settingsMenu;
        public GameObject settingsMenuObject;

        public void Awake()
        {
            SceneManager.LoadScene("SettingsMenu", LoadSceneMode.Additive);
        }
        private void Start()
        {
            settingsMenuObject = GameObject.Find("SettingsMenuCanvas");
            if (settingsMenuObject != null)
            {
                settingsMenu = settingsMenuObject.GetComponent<SettingsMenu>();
            }
        }

        /// <summary>
        /// Toggles the display of the settings menu
        /// </summary>
        public void ToggleSettings()
        {
            settingsMenu.ToggleActive(this.gameObject);
        }

        /// <summary>
        /// On click method for play button 
        /// </summary>
        public void PlayGame()
        {
            // Scene must be added to build settings to be loaded
            SceneManager.LoadScene("defcon city");
        }

        /// <summary>
        /// On click method for credits button
        /// </summary>
        public void CreditsMenu()
        {
            SceneManager.LoadScene("Credits");
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
    }
}