using DefconZ.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefconZ.UI
{
    /// <summary>
    /// Stores reference to accescible UI elements
    /// </summary>
    public class InGameUI : MonoBehaviour
    {
        public PlayerUI playerUI;
        public PauseMenu pauseUI;
        public SelectionUI selectionUI;
        public VictoryScreen victoryScreen;
        public Player player;
        public SpecialAbilitiesUI specialAbilitiesUI;

        public void InitUI(Faction faction)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            player.inGameUI = this;
            playerUI.InitUI(faction);
            player.objectSelector.player = player;
            player.objectSelector.cam = player.cam;
            player.objectSelector.selectionUI = selectionUI;
        }

        public void EndGameScreen(Faction looser, Faction winner)
        {
            GameObject.Find("Game Music").SetActive(false);
            victoryScreen.DisplayVictory(looser, winner);
        }
        
        /// <summary>
        /// Post initialisation for the InGameUI
        /// </summary>
        public void PostInitUI()
        {
            playerUI.PostInit();
        }
    }
}