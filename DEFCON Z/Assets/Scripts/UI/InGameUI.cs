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
        public SpecialAbilitiesUI specialAbilitiesUI;

        public void InitUI(Faction faction)
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            playerUI.InitUI(faction);
            player.objectSelector.player = player;
            player.objectSelector.cam = player.cam;
            player.objectSelector.selectionUI = selectionUI;
        }
    }
}