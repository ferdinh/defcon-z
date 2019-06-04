using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.UI
{
    public class PerksUI : MonoBehaviour
    {
        public Player player;
        public GameObject PerksList;

        private void Awake()
        {
            TogglePanelAction();
        }

        public void SmallResourceBoostButton()
        {
            if (player.CanAfford(500f))
            {
                player.playerFaction.Resource.ResourcePoint -= 500f;
                player.playerFaction.Modifiers.Add(Perks.SmallResourceBoost);
            }

            TogglePanelAction();
        }

        public void MediumResourceBoostButton()
        {
            if (player.CanAfford(1000f))
            {
                player.playerFaction.Resource.ResourcePoint -= 1000f;
                player.playerFaction.Modifiers.Add(Perks.MediumResourceBoost);
            }

            TogglePanelAction();
        }

        public void LargeResourceBoostButton()
        {
            if (player.CanAfford(2000f))
            {
                player.playerFaction.Resource.ResourcePoint -= 2000f;
                player.playerFaction.Modifiers.Add(Perks.LargeResourceBoost);
            }

            TogglePanelAction();
        }

        public void TogglePanelAction()
        {
            bool state = (PerksList.activeSelf) ? false : true;
            PerksList.SetActive(state);
        }
    }
}