using DefconZ.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.GameLevel
{
    public class Zone : MonoBehaviour
    {
        public ZoneManager zoneManager;
        public Faction zoneOwner;
        public float zoneResourceValue;

        [SerializeField]
        private Material zoneMaterial;

        [SerializeField]
        private List<UnitBase> unitsInZone;

        public GameObject friendlyDisplay;
        public GameObject enemyDisplay;
        public GameObject neutralDisplay;

        private void Awake()
        {
            unitsInZone = new List<UnitBase>();
        }

        /// <summary>
        /// Initialises the zone
        /// </summary>
        /// <param name="zoneManager"></param>
        /// <param name="faction"></param>
        public void Init(ZoneManager zoneManager, Faction faction)
        {
            this.zoneManager = zoneManager;
            zoneMaterial = gameObject.transform.GetComponentInChildren<MeshRenderer>().material;
            zoneMaterial.color = zoneManager.neutralColor;
            SetZoneOwner(faction);

            gameObject.layer = 2; // Sets the gameobject layer to ignore raycasts
        }

        /// <summary>
        /// Updates the current zone
        /// Iterates through units inside zone and calculates the correct owner of the zone
        /// </summary>
        private void UpdateZone()
        {
            Faction owner = null;
            bool multipleFactions = false;
            
            // Calculate the current owner of the zone
            foreach (UnitBase unit in unitsInZone)
            {
                if (owner == null)
                {
                    owner = unit.FactionOwner;
                }
                else
                {
                    // Check if the current calculated owner is different to the unit
                    if (unit.FactionOwner != owner)
                    {
                        multipleFactions = true;
                    }
                }
            }

            // Set the zones owner
            // If no units are in the zone, do not change the owner
            if (unitsInZone.Count > 0)
            {
                // If multiple factions are present, there is no owner of the zone
                if (multipleFactions)
                {
                    SetZoneOwner(null);
                }
                else
                {
                    SetZoneOwner(owner);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            UnitBase unit = other.GetComponent<UnitBase>();
            if (unit != null)
            {
                Debug.Log(unit.objName + " entered zone");
                unitsInZone.Add(unit);
                unit.currentZone = this;
                UpdateZone();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            UnitBase unit = other.GetComponent<UnitBase>();
            if (unit != null)
            {
                Debug.Log(unit.objName + " exited zone");

                // Remove the unit from the zone
                RemoveFromZone(unit);

                UpdateZone();
            }
        }

        /// <summary>
        /// Returns the faction that controls the zone
        /// </summary>
        /// <returns></returns>
        protected Faction GetZoneOwner()
        {
            return zoneOwner;
        }

        /// <summary>
        /// Sets the owner of the zone.
        /// Updates the visual display of the zone.
        /// </summary>
        /// <param name="faction"></param>
        public void SetZoneOwner(Faction faction)
        {
            // Check that the current owner is not the same as the new owner
            if (zoneOwner != faction)
            {
                // Check if the current zone is owned
                if (zoneOwner != null)
                {
                    // Remove the resource cap boost for the owner of the zone
                    zoneOwner.Resource.OwnedZones.Remove(this);
                }

                // Assign the new zone owner
                zoneOwner = faction;

                // Check if a new faction owner is present
                if (zoneOwner != null)
                {
                    // Add the resource cap boost to the new owner
                    zoneOwner.Resource.OwnedZones.Add(this);
                }

                // Update the display color of the zone
                UpdateZoneColor();
            }
        }

        /// <summary>
        /// Updates the display color for the zone
        /// </summary>
        /// <param name="faction"></param>
        public void UpdateZoneColor()
        {
            if (zoneOwner == null)
            {
                neutralDisplay.SetActive(true);
                friendlyDisplay.SetActive(false);
                enemyDisplay.SetActive(false);
            }
            else if (zoneOwner.IsPlayerUnit)
            {
                neutralDisplay.SetActive(false);
                friendlyDisplay.SetActive(true);
                enemyDisplay.SetActive(false);
            }
            else
            {
                neutralDisplay.SetActive(false);
                friendlyDisplay.SetActive(false);
                enemyDisplay.SetActive(true);
            }

            if (!zoneManager.zoneDisplayActive)
            {
                neutralDisplay.SetActive(false);
                friendlyDisplay.SetActive(false);
                enemyDisplay.SetActive(false);
            }
        }

        /// <summary>
        /// Checks if the given unit is inside the zone
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>True if the given unit is inside the zone</returns>
        public bool IsInsideZone(UnitBase unit)
        {
            return unitsInZone.Contains(unit);
        }

        /// <summary>
        /// Removes the given unit from the zone list
        /// </summary>
        /// <param name="unit"></param>
        public void RemoveFromZone(UnitBase unit)
        {
            if (unitsInZone.Contains(unit))
            {
                unitsInZone.Remove(unit);
            }

            // Call an update to the zone
            UpdateZone();
        }
    }
}