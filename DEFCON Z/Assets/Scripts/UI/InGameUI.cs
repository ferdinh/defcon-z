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
        public Text nameLabel;
        public Text healthLabel;
        public Text factionLabel;
        public Text pointStatusLabel;

        public SliderBar pointStatus;

        public Text levelStatusLabel;

        public Color defaultColor;
        public Color friendlyColor;
        public Color enemyColor;

		private Faction playerFaction;

        public void FixedUpdate()
        {
            UpdateResourcePoint();
        }

        /// <summary>
        /// Initialises the in game UI
        /// Must be called after the game manager has finished initialisation
        /// </summary>
        public void InitUI(Faction playerFaction)
		{
			this.playerFaction = playerFaction;
            pointStatus.InitSliderBar(playerFaction.Resource.MaxResourcePoint, 0.0f);
		}

        /// <summary>
        /// Updates the UI Selection area of the UI from the given object
        /// </summary>
        /// <param name="obj"></param>
        /// 
        public void UpdateResourcePoint()
        {
            if (playerFaction != null)
            {
                pointStatus.UpdateSlider(playerFaction.Resource.ResourcePoint);
            }
        }
        public void UpdateObjectSelectionUI(ObjectBase obj)
        {
            // check if we have an object
            if (obj != null)
            {
                nameLabel.text = obj.objName;

                // check if the object is a unit
                UnitBase _selectedUnit = obj.GetComponent<UnitBase>();
                if (_selectedUnit != null)
                {
                    healthLabel.text = "HP: " + _selectedUnit.health.ToString();
                    factionLabel.text = _selectedUnit.FactionOwner.FactionName;

                    // check if the unit is friendly and set appropriate color
                    factionLabel.color = (_selectedUnit.FactionOwner.IsPlayerUnit) ? friendlyColor : enemyColor;
                }
                else
                {
                    // check if the object is a prop
                    Prop _selectedProp = obj.GetComponent<Prop>();
                    if (_selectedProp != null)
                    {
                        healthLabel.text = "HP: " + _selectedProp.health.ToString();
                        factionLabel.text = "World Object";
                        factionLabel.color = defaultColor;
                    }
                }
            }
            else
            {
                // set the object selection UI to blank state
                nameLabel.text = "N/A";
                healthLabel.text = "HP: ";
                factionLabel.text = "";
            }
        }

		/// <summary>
		/// Action when purchase unit button is pressed
		/// </summary>
		public void PurchaseUnitAction()
		{
			playerFaction.RecruitUnit();
		}
    }
}