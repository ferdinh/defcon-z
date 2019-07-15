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
        public Color loserColor;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Activates and updates the victory screen panel
        /// </summary>
        /// <param name="loser"></param>
        /// <param name="winner"></param>
        public void DisplayVictory(Faction loser, Faction winner)
        {
            gameObject.SetActive(true);

            winnerLabel.text = winner.FactionName;
            winnerLabel.color = (winner.IsPlayerUnit) ? winnerColor : loserColor;

            string victoryText = $"{winner.FactionName} has won the game!\nShame on {loser.FactionName} for losing!";
            description.text = victoryText;
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