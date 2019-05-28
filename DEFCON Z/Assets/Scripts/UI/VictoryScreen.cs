using DefconZ.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefconZ.UI
{
    public class VictoryScreen : MonoBehaviour
    {
        public Text winnerLabel;
        public Text description;

        public Color winnerColor;
        public Color looserColor;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Activates and updates the victory screen panel
        /// </summary>
        /// <param name="looser"></param>
        /// <param name="winner"></param>
        public void DisplayVictory(Faction looser, Faction winner)
        {
            gameObject.SetActive(true);

            winnerLabel.text = winner.FactionName;
            winnerLabel.color = (winner.IsPlayerUnit) ? winnerColor : looserColor;

            string victoryText = $"{winner.FactionName} has won the game!\nShame on {looser.FactionName} for loosing!";description.text = victoryText;
        }

        /// <summary>
        /// Loads the credits menu
        /// </summary>
        public void CreditsMenuButton()
        {
            SceneManager.LoadScene("Credits");
        }
    }
}