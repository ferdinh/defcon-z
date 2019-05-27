using DefconZ.Units.Special;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.UI
{
    public class SpecialAbilitiesUI : MonoBehaviour
    {
        public Player player;
        public GameObject AbilityList;

        private void Awake()
        {
            TogglePanelAction();
        }

        public void AirStrikeAction()
        {
            player.selectedAction = true;
            player.selectedAbility = AbilityType.PrecisionBomb;
        }

        public void TogglePanelAction()
        {
            bool state = (AbilityList.activeSelf) ? false : true;
            AbilityList.SetActive(state);
        }
    }
}