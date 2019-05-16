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

        [SerializeField]
        private Material zoneMaterial;

        [SerializeField]
        private List<UnitBase> unitsInZone;

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

            SetZoneOwner(faction);

            gameObject.layer = 2; // Sets the gameobject layer to ignore raycasts
        }

        /// <summary>
        /// Updates the current zone
        /// Itterates through units inside zone and calculates the correct owner of the zone
        /// </summary>
        private void UpdateZone()
        {
            Faction _owner = null;
            bool _multipleFactions = false;
            int _unitCount = 0;
            
            // Calculate the current owner of the zone
            foreach (UnitBase _unit in unitsInZone)
            {
                if (_owner == null)
                {
                    _owner = _unit.FactionOwner;
                }
                else
                {
                    // Check if the current calculated owner is different to the unit
                    if (_unit.FactionOwner != _owner)
                    {
                        _multipleFactions = true;
                    }
                    else if (_owner == null)
                    {
                        _owner = _unit.FactionOwner;
                    }
                }

                _unitCount++;
            }

            // Set the zones owner
            // If no units are in the zone, do not change the owner
            if (_unitCount > 0)
            {
                // If multiple factions are present, there is no owner of the zone
                if (_multipleFactions)
                {
                    SetZoneOwner(null);
                }
                else
                {
                    SetZoneOwner(_owner);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            UnitBase _unit = other.GetComponent<UnitBase>();
            if (_unit != null)
            {
                Debug.Log(_unit.objName + " entered zone");
                unitsInZone.Add(_unit);
                _unit.currentZone = this;
                UpdateZone();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            UnitBase _unit = other.GetComponent<UnitBase>();
            if (_unit != null)
            {
                Debug.Log(_unit.objName + " exited zone");

                // Remove the unit from the zone
                RemoveFromZone(_unit);

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
            zoneOwner = faction;
            if (faction == null)
            {
                zoneMaterial.color = zoneManager.neutralColor;
            }
            else if (faction.IsPlayerUnit)
            {
                zoneMaterial.color = zoneManager.friendlyColor;
            }
            else
            {
                zoneMaterial.color = zoneManager.enemyColor;
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

            UpdateZone();
        }
    }
}