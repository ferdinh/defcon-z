using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.Units
{
    public class Faction : MonoBehaviour
    {
        public string FactionName { get; set; }
        public IList<GameObject> Units { get; set; }
        public FactionType FactionType { get; set; }
        public bool IsHumanPlayer { get; set; } = false;

        private void Awake()
        {
            Units = new List<GameObject>();
        }

}
