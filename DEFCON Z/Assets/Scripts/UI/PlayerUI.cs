using DefconZ.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefconZ.UI
{
    public class PlayerUI : MonoBehaviour
    {
        public Text nameLabel;
        public Text healthLabel;
        public Text factionLabel;
        public Text pointStatusLabel;
        public Text resourceGainLabel;
        public Text playerLevelLabel;
        public float previousResourcePoints;

        public SliderBar pointStatus;

        public Text gameDayLabel;

        public Color defaultColor;
        public Color friendlyColor;
        public Color enemyColor;

        public Faction playerFaction;
        [SerializeField]
        private Player player;

        private Clock clock;

        public SpecialAbilitiesUI abilitiesUI;

        private void Awake()
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }

        /// <summary>
        /// Post initialisation for the player UI
        /// Manually updates UI references for first frame.
        /// </summary>
        public void PostInit()
        {
            if (clock == null)
            {
                clock = GameObject.FindGameObjectWithTag(nameof(GameManager)).GetComponent<Clock>();
            }

            // Update the UI on the first game frame
            UpdateResourcePoint(null, null);
            UpdateSelectionDisplayEvent(null, null);
            UpdateGameDayLabelEvent(null, null);
            UpdatePlayerLevelLabelEvent(null, null);
        }

        /// <summary>
        /// Subscribes the player UI to the game clock for updates.
        /// </summary>
        public void GameClockSubscribe()
        {
            if (clock == null)
            {
                clock = GameObject.FindGameObjectWithTag(nameof(GameManager)).GetComponent<Clock>();
            }
            
            clock.GameCycleElapsed += UpdateResourcePoint;
            clock.GameCycleElapsed += UpdateSelectionDisplayEvent;
            clock.GameCycleElapsed += UpdateGameDayLabelEvent;
            clock.GameCycleElapsed += UpdatePlayerLevelLabelEvent;
        }

        /// <summary>
        /// Initialises the in game UI
        /// Must be called after the game manager has finished initialisation
        /// </summary>
        public void InitUI(Faction playerFaction)
        {
            this.playerFaction = playerFaction;
            player.playerFaction = playerFaction;
            pointStatus.InitSliderBar(playerFaction.Resource.GetMaxResourcePoint, 0.0f);
            previousResourcePoints = playerFaction.Resource.ResourcePoint;
            abilitiesUI.player = player;
        }

        /// <summary>
        /// Updates the UI Selection area of the UI from the given object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateResourcePoint(object sender, System.EventArgs e)
        {
            if (playerFaction != null)
            {
                pointStatus.UpdateSlider(playerFaction.Resource.ResourcePoint, playerFaction.Resource.GetMaxResourcePoint);
                float netGain = playerFaction.Resource.ResourcePoint - previousResourcePoints;
                previousResourcePoints = playerFaction.Resource.ResourcePoint;
                resourceGainLabel.text = netGain.ToString("+#;-#;+0");

                // setting the colour based on the value of netGain.
                resourceGainLabel.color = (netGain >= 0) ? friendlyColor : enemyColor;
            }
        }

        /// <summary>
        /// Updates the object selection via event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateSelectionDisplayEvent(object sender, System.EventArgs e)
        {
            UpdateObjectSelectionUI();
        }

        /// <summary>
        /// Updates Game Day label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateGameDayLabelEvent(object sender, System.EventArgs e)
        {
            gameDayLabel.text = clock.GameDay.ToString();
        }

        /// <summary>
        /// Updates Level Label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdatePlayerLevelLabelEvent(object sender, System.EventArgs e)
        {
            playerLevelLabel.text = "Level: " + playerFaction.Level.CurrentLevel.ToString();
        }

        /// <summary>
        /// Updates the object selection panel information display.
        /// </summary>
        public void UpdateObjectSelectionUI()
        {
            GameObject selectedObject;

            if (player.selectedObjects.Count > 0)
            {
                selectedObject = player.selectedObjects[0];
            }
            else
            {
                selectedObject = null;
            }
            
            ObjectBase _object = null;

            if (selectedObject != null)
            {
                _object = selectedObject.GetComponent<ObjectBase>();
            }

            // check if we have an object
            if (_object != null)
            {
                nameLabel.text = _object.objName;

                // check if the object is a unit
                UnitBase _selectedUnit = _object.GetComponent<UnitBase>();
                if (_selectedUnit != null)
                {
                    //healthLabel.text = "HP: " + _selectedUnit.health.ToString();
                    healthLabel.text = $"HP: {_selectedUnit.health.ToString("n0")}/{_selectedUnit.maxHealth.ToString("n0")}";
                    factionLabel.text = _selectedUnit.FactionOwner.FactionName;

                    // check if the unit is friendly and set appropriate color
                    factionLabel.color = (_selectedUnit.FactionOwner.IsPlayerUnit) ? friendlyColor : enemyColor;
                }
                else
                {
                    // check if the object is a prop
                    Prop _selectedProp = _object.GetComponent<Prop>();
                    if (_selectedProp != null)
                    {
                        //=healthLabel.text = "HP: " + _selectedProp.health.ToString("n0");
                        healthLabel.text = $"HP: {_selectedProp.health.ToString("n0")}/{_selectedProp.maxHealth.ToString("n0")}";
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

        /// <summary>
        /// Executes the test code bellow.
        /// The functionality of this test button may change depending on the needs of the current branch
        /// Function is not final, and will not be accessible in final builds.
        /// </summary>
        public void TestButtonAction()
        {
            Debug.LogError($"{Time.time}: Using test button");
            playerFaction.Level.AddXP(50);
        }
    }
}