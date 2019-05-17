using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.GameLevel
{
    public class CustomZoneContainer : MonoBehaviour
    {
        public ZoneManager zoneManager;

        public List<Zone> customZones;
        // Start is called before the first frame update
        void Start()
        {
            if (zoneManager != null)
            {
                // check that the managed zones list is initialised
                if (zoneManager.managedZones != null)
                {
                    AddZonesToManager();
                }
                else
                {
                    Debug.LogError(zoneManager.name + " does not have an initialised managedZones list");
                }
            }
            else
            {
                Debug.LogError("Zone Manager not configured for: " + name);
            }
        }

        /// <summary>
        /// Adds the zones stored by the custom zone container to the manager
        /// </summary>
        private void AddZonesToManager()
        {
            foreach (Zone _zone in customZones)
            {
                _zone.Init(zoneManager, null);
                zoneManager.managedZones.Add(_zone);
            }
        }
    }
}