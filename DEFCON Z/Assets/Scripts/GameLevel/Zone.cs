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
                SetZoneOwner(_unit.FactionOwner);
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
            }
        }

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

        public bool IsInsideZone(UnitBase unit)
        {
            return unitsInZone.Contains(unit);
        }

        public void RemoveFromZone(UnitBase unit)
        {
            if (unitsInZone.Contains(unit))
            {
                unitsInZone.Remove(unit);
            }
        }
    }
}