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
        public bool IsHumanPlayer { get; set; } = false;
        public GameObject UnitPrefab { get; internal set; }

        private void Awake()
        {
            Resource = new Resource();

            Resource.CalculateMaxPoints();
            Resource.ComputeStartingValue();

            Units = new List<GameObject>();
        }

}
