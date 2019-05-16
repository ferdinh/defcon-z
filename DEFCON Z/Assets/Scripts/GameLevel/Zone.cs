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
        /// 
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
        /// 
        /// </summary>
        private void UpdateZone()
        {
            Faction _owner = null;
            bool _multipleFactions = false;
            
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
            }

            // Set the zones owner
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            UnitBase _unit = other.GetComponent<UnitBase>();
            if (_unit != null)
            {
                Debug.LogError(_unit.objName + " entered zone");
                unitsInZone.Add(_unit);
                _unit.currentZone = this;
                UpdateZone();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            UnitBase _unit = other.GetComponent<UnitBase>();
            if (_unit != null)
            {
                Debug.LogError(_unit.objName + " exited zone");

                // Remove the unit from the zone
                RemoveFromZone(_unit);

                UpdateZone();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected Faction GetZoneOwner()
        {
            return zoneOwner;
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool IsInsideZone(UnitBase unit)
        {
            return unitsInZone.Contains(unit);
        }

        /// <summary>
        /// 
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