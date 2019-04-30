using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefconZ
{
    public class MainMenu : MonoBehaviour
    {
        public Scene testLevel;
        public bool settings;
        public GameObject settingsPanel, menuPanel;
        [SerializeField]
        private Vector3 panelPosition, panelHiddenPosition;
        //private AssetBundle levels;

        // Start is called before the first frame update
        void Start()
        {
            panelPosition = settingsPanel.transform.position;
            panelHiddenPosition = panelPosition;
            panelHiddenPosition.x = Screen.width * 2;

            settingsPanel.transform.position = panelHiddenPosition;

            settings = false;
            //levels = AssetBundle.LoadFromFile("Assets/AssetBundles/Scenes");
        }

        /// <summary>
        /// Toggles the display of the settings menu
        /// </summary>
        public void toggleSettings()
        {
            // Flip the settings bool state
            settings = (settings) ? false : true;

            if (settings)
            {
                // Move settings into view, main out of view
                settingsPanel.transform.position = panelPosition;
                menuPanel.transform.position = panelHiddenPosition;
            }
            else
            {
                // Hides settings into view position
                settingsPanel.transform.position = panelHiddenPosition;
                menuPanel.transform.position = panelPosition;
            }
        }

        /// <summary>
        /// On click method for settings button 
        /// </summary>
        public void SettingsButton()
        {
            SceneManager.LoadScene("ObjectSelectionScene");
        }
        /// <summary>
        /// On click method for play button 
        /// </summary>
        public void PlayGame()
        {
            // Scene must be added to build settings to be loaded
            SceneManager.LoadScene("ObjectSelectionScene");
        }
        /// <summary>
        /// On click method for Quit button 
        /// </summary>
        public void QuitGame()
        {
            Debug.Log("Quitting game!");
            Application.Quit();
        }
    }
}

