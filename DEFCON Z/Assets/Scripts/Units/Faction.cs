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
        public GameObject UnitSpawnPoint;

        private void Awake()
        {
            Units = new List<GameObject>();
            Level = new Level();
            Resource = new Resource();
            InitAwake();
        }

        private void Start()
        {
            // Reference the faction level to the resource calculation.
            Resource.Modifiers.Add(Level.LevelModifier);

            Resource.CalculateMaxPoints()
                    .ComputeStartingValue();

            Debug.Log(FactionName + " faction has Max Resource Point of " + Resource.MaxResourcePoint);
            Debug.Log(FactionName + " faction has Starting Resource Point of " + Resource.ResourcePoint);

            InitStart();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected virtual void InitAwake()
        {
        }

        /// <summary>
        /// Initializes this instance when Start is called or when game object
        /// is active.
        /// </summary>
        protected virtual void InitStart()
        {
        }

        /// <summary>
        /// Recruits a new unit when there is enough resource for the
        /// faction.
        /// </summary>
        public void RecruitUnit()
        {
            if (CanRecruitUnit())
            {
                // Create the unit.
                var newUnit = Instantiate(UnitPrefab, UnitSpawnPoint.transform.position, Quaternion.identity);
                newUnit.GetComponent<UnitBase>().FactionOwner = this;

                // Consume the resource when creating.
                Resource.UseResource(newUnit.GetComponent<UnitBase>().RecruitCost);

                Units.Add(newUnit);
            }
        }

        /// <summary>
        /// Determines whether this faction has enough resource point to
        /// recruit a unit.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this faction [can recruit unit]; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRecruitUnit()
        {
            bool canRecruitUnit = UnitPrefab.GetComponent<UnitBase>().RecruitCost <= Resource.ResourcePoint;

            if (canRecruitUnit)
            {
                Debug.Log(this.FactionName + " have enough point to recruit new unit");
            }
            else
            {
                Debug.Log(FactionName + " don't have enough point to recruit new unit");
            }

            return canRecruitUnit;
        }
    }
}