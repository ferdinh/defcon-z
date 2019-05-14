using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefconZ.GameLevel
{
    public class GameLevel : MonoBehaviour
    {
        public string inGameUIScene;
        public Player player;
        public GameManager gameManager;
        public InGameUI inGameUI;

        private Scene loadedUIScene;

        void Awake()
        {
            gameManager = gameObject.GetComponent<GameManager>();

            LoadInGameUI();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadInGameUI()
        {
            if (inGameUIScene != "")
            {
                StartCoroutine(LoadLevel());
            }
            else
            {
                Debug.LogError("No game UI scene defined");
            }
        }

        /// <summary>
        /// Waits for the specified scene to be loaded before calling to update the player UI reference
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadLevel()
        {
            Debug.Log("Starting in game UI scene loading");

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(inGameUIScene, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            Debug.Log("Game UI Loaded");
            UpdatePlayerUIReference();
        }

        /// <summary>
        /// Updates the player with reference to the in game UI
        /// </summary>
        private void UpdatePlayerUIReference()
        {
            Debug.Log("UI Scene has loaded now!");
            inGameUI = GameObject.Find("InGameUI").GetComponent<InGameUI>();

            if (inGameUI != null)
            {
                player.playerUI = inGameUI;
            }
            else
            {
                Debug.LogError("In Game UI could not be found");
            }
        }
    }
}