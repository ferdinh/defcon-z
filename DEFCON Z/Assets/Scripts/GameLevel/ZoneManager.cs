using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.GameLevel
{
    public class ZoneManager : MonoBehaviour
    {
        public GameObject zonePrefab;
        public bool generateZones;

        public int worldWidth;
        public int worldHeight;
        public float zoneSpacing;
        public Color neutralColor, friendlyColor, enemyColor;
        public List<Zone> managedZones;
        public bool zoneDisplayActive;

        private void Awake()
        {
            managedZones = new List<Zone>();
            zoneDisplayActive = true;

            // Add pre-built zones to list of managed zones
        }

        // Start is called before the first frame update
        void Start()
        {
            // Generate zones if the manager is flagged to
            if (generateZones)
            {
                GenerateLevelZones();
            }

            ToggleZoneDisplay();
        }

        /// <summary>
        /// Generates the managed zones for the zone manager
        /// </summary>
        private void GenerateLevelZones()
        {
            ZoneManager zoneManager = this;
            // generate rows
            for (int i = 0; i < worldWidth; i++)
            {
                // generate columns
                for (int j = 0; j < worldHeight; j++)
                {
                    float xPos = zoneSpacing * i;
                    float zPos = zoneSpacing * j;

                    GameObject zoneObject = Instantiate(zonePrefab, new Vector3(xPos, 0, zPos), Quaternion.identity); // Create the zone
                    Zone zone = zoneObject.GetComponent<Zone>();
                    zone.Init(zoneManager, null); // Initialise the zone
                    zone.transform.parent = gameObject.transform; // Set the zones parent to be the zone manager, this stops the game heierachy being clogged
                    managedZones.Add(zone); // Add the generated zone to the managed zones list
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddZone(Zone zone)
        {
            zone.Init(this, null); // Initialise the zone
            zone.transform.parent = gameObject.transform;
            managedZones.Add(zone);
        }

        /// <summary>
        /// Toggles the display of the zones to the player
        /// Does not effect simulation
        /// </summary>
        public void ToggleZoneDisplay ()
        {
            zoneDisplayActive = (zoneDisplayActive) ? false : true;

            foreach (Zone zone in managedZones)
            {
                zone.UpdateZoneColor();
            }
        }
    }
}