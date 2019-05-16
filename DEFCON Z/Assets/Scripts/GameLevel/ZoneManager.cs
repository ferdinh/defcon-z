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

        [SerializeField]
        private List<Zone> managedZones;
        [SerializeField]
        private bool zoneDisplayActive;

        private void Awake()
        {
            managedZones = new List<Zone>();
            zoneDisplayActive = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            // Generate zones if the manager is flagged to
            if (generateZones)
            {
                GenerateLevelZones();
            }
        }

        /// <summary>
        /// Generates the managed zones for the zone manager
        /// </summary>
        private void GenerateLevelZones()
        {
            ZoneManager _zoneManager = this;
            // generate rows
            for (int i = 0; i < worldWidth; i++)
            {
                // generate columns
                for (int j = 0; j < worldHeight; j++)
                {
                    float _xPos = zoneSpacing * i;
                    float _zPos = zoneSpacing * j;

                    GameObject _zoneObject = Instantiate(zonePrefab, new Vector3(_xPos, 0, _zPos), Quaternion.identity); // Create the zone
                    Zone _zone = _zoneObject.GetComponent<Zone>();
                    _zone.Init(_zoneManager, null); // Initialise the zone
                    _zone.transform.parent = gameObject.transform; // Set the zones parent to be the zone manager, this stops the game heierachy being clogged
                    managedZones.Add(_zone); // Add the generated zone to the managed zones list
                }
            }
        }

        /// <summary>
        /// Toggles the display of the zones to the player
        /// Does not effect simulation
        /// </summary>
        public void ToggleZoneDisplay ()
        {
            zoneDisplayActive = (zoneDisplayActive) ? false : true;

            foreach (Zone _zone in managedZones)
            {
                _zone.GetComponentInChildren<MeshRenderer>().enabled = zoneDisplayActive;
            }
        }
    }
}