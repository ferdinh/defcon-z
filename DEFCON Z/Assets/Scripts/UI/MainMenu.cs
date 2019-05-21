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
        public bool settings;
        public GameObject settingsPanel;
        public GameObject menuPanel;

        public void Awake()
        {
            // Make sure the menu panel is shown and the settings panel is hidden by default
            menuPanel.SetActive(true);
            settingsPanel.SetActive(false);
        }

        /// <summary>
        /// Toggles the display of the settings menu
        /// </summary>
        public void ToggleSettings()
        {
            // Flip the settings bool state
            settings = (settings) ? false : true;

            if (settings)
            {
                // Hides menu panel
                settingsPanel.SetActive(true);
                menuPanel.SetActive(false);
            }
            else
            {
                // Hides settings panel
                settingsPanel.SetActive(false);
                menuPanel.SetActive(true);
            }
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