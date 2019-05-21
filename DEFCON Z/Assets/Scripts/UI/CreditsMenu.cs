using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefconZ.UI
{
    public class CreditsMenu : MonoBehaviour
    {
        public float speed;
        public Text creditsText;
        public TextAsset creditsFile;
        public GameObject creditsContainer;

        void Awake()
        {
            creditsText.text = creditsFile.text;
        }

        // Update is called once per frame
        void Update()
        {
            // Moves the credits container up by the defined speed
            creditsContainer.transform.Translate(Vector3.up * Time.deltaTime * speed);
        }

        /// <summary>
        /// On click method for mainmenu button 
        /// </summary>
        public void returnMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
