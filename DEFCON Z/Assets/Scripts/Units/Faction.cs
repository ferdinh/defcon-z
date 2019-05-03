using DefconZ.Simulation;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.Units
{
    public class Faction : MonoBehaviour
    {
        public string FactionName { get; set; }
        public Resource Resource { get; set; }
        public IList<GameObject> Units { get; set; }
        public FactionType FactionType { get; set; }
        public bool IsPlayerUnit { get; set; } = false;
        public GameObject UnitPrefab { get; internal set; }
        public Level Level { get; set; }

        private void Awake()
        {
            Units = new List<GameObject>();
            Level = new Level();

            Resource = new Resource();

            Resource.CalculateMaxPoints();
            Resource.ComputeStartingValue();

            Debug.Log(FactionName + " faction has Max Resource Point of " + Resource.MaxResourcePoint);
            Debug.Log(FactionName + " faction has Starting Resource Point of " + Resource.ResourcePoint);
        }

        private void Start()
        {
            // Reference the faction level to the resource calculation.
            Resource.Modifiers.Add(Level.LevelModifier);
        }
    }
}
