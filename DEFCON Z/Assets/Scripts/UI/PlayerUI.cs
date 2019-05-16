using DefconZ.Units;
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

        public SliderBar pointStatus;

        public Text levelStatusLabel;

        public Color defaultColor;
        public Color friendlyColor;
        public Color enemyColor;

        private Faction playerFaction;

        [SerializeField]
        private Player player;

        private void Awake()
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            player.selectedObject = null;
        }

        /// <summary>
        /// Subscribes the player UI to the game clock for updates.
        /// </summary>
        public void GameClockSubscribe()
        {
            var clock = GameObject.FindGameObjectWithTag(nameof(GameManager)).GetComponent<Clock>();
            clock.GameCycleElapsed += UpdateResourcePoint;
            clock.GameCycleElapsed += UpdateSelectionDisplayEvent;
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
        public void UpdateResourcePoint(object sender, System.EventArgs e)
        {
            if (playerFaction != null)
            {
                pointStatus.UpdateSlider(playerFaction.Resource.ResourcePoint);
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
        /// Updates the object selection panel information display.
        /// </summary>
        public void UpdateObjectSelectionUI()
        {
            GameObject _gameObject = player.selectedObject;
            ObjectBase _object = null;

            if (_gameObject != null)
            {
                _object = _gameObject.GetComponent<ObjectBase>();
            }

            // check if we have an object
            if (_object != null)
            {
                nameLabel.text = _object.objName;

                // check if the object is a unit
                UnitBase _selectedUnit = _object.GetComponent<UnitBase>();
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
                    Prop _selectedProp = _object.GetComponent<Prop>();
                    if (_selectedProp != null)
                    {
                        healthLabel.text = "HP: " + _selectedProp.health.ToString("n0");
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