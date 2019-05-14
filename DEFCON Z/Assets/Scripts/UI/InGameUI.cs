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
        public GameObject playerUI;
        public GameObject pauseUI;

        public void InitUI(Faction faction)
        {
            playerUI.GetComponent<PlayerUI>().InitUI(faction);
        }

    }
}