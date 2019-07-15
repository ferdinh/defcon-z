using DefconZ.GameLevel;
using DefconZ.Simulation;
using DefconZ.Simulation.UnitBuilder;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.Units
{
    public class Faction : MonoBehaviour
    {
        public string FactionName { get; set; }
        public Resource Resource { get; set; }
        public IList<GameObject> Units { get; set; }
        public IList<Zone> Zones { get; set; }
        public FactionType FactionType { get; set; }
        public bool IsPlayerUnit { get; set; } = false;
        public GameObject UnitPrefab { get; internal set; }
        public Level Level { get; set; }
        public GameObject UnitSpawnPoint;

        public ICollection<Modifier> Modifiers;

        public UnitBuilder unitBuilder;

        private void Awake()
        {
            Units = new List<GameObject>();
            Level = new Level();
            Modifiers = new List<Modifier>();
            Resource = new Resource(Modifiers);
            unitBuilder = gameObject.AddComponent<UnitBuilder>();

            Debug.Log(FactionName + " faction has Max Resource Point of " + Resource.GetMaxResourcePoint);
            Debug.Log(FactionName + " faction has Starting Resource Point of " + Resource.ResourcePoint);
            Debug.Log($"{FactionName} faction has Starting level of {Level.CurrentLevel}");

            unitBuilder.OnBuildStart += StartBuild;
            unitBuilder.OnBuildFinish += AfterBuild;

            InitAwake();
        }

        /// <summary>
        /// Runs after the unit build is completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BuildFinishedEventArgs"/> instance containing the event data.</param>
        private void AfterBuild(object sender, BuildFinishedEventArgs e)
        {
            var newUnit = e.createdUnit;
            newUnit.GetComponent<UnitBase>().FactionOwner = this;
            Units.Add(newUnit);

            Debug.Log("Finish building " + newUnit.GetComponent<UnitBase>().objName);
        }

        public void SpawnFactionUnit(GameObject prefab, Vector3 spawnPoint)
        {
            var newUnit = Instantiate(prefab, spawnPoint, Quaternion.identity);
            newUnit.GetComponent<UnitBase>().FactionOwner = this;
            Units.Add(newUnit);
        }

        /// <summary>
        /// Starts the unit building.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void StartBuild(object sender, EventArgs e)
        {
            Debug.Log("Start building unit");
        }

        private void Start()
        {
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
            ForceRecruitUnit();
        }

        /// <summary>
        /// Recruits the unit at the target position when there is enough
        /// resources.
        /// </summary>
        /// <param name="spawnPoint">The spawn point.</param>
        public void RecruitUnitAt(Vector3 spawnPoint)
        {
            if (CanRecruitUnit(UnitPrefab.GetComponent<UnitBase>().RecruitCost))
            {
                unitBuilder.AddToBuildQueue(new UnitOrder(UnitPrefab, spawnPoint));

                // Consume the resource when the build had started.
                Resource.UseResource(UnitPrefab.GetComponent<UnitBase>().RecruitCost);
            }
        }

        public void RecruitUnitAt(Vector3 spawnPoint, GameObject prefab)
        {
            if (CanRecruitUnit(UnitPrefab.GetComponent<UnitBase>().RecruitCost))
            {
                unitBuilder.AddToBuildQueue(new UnitOrder(prefab, spawnPoint));

                // Consume the resource when the build had started.
                Resource.UseResource(UnitPrefab.GetComponent<UnitBase>().RecruitCost);
            }
        }

        /// <summary>
        /// Recruit unit by bypassing resources and build time.
        /// Only for initializing.
        /// </summary>
        private void ForceRecruitUnit()
        {
            var recruitedUnit = Instantiate(UnitPrefab, UnitSpawnPoint.transform.position, Quaternion.identity);
            recruitedUnit.GetComponent<UnitBase>().FactionOwner = this;

            Units.Add(recruitedUnit);
        }

        /// <summary>
        /// Recruit unit by bypassing resources and build time.
        /// Only for initializing.
        /// </summary>
        public void ForceRecruitUnit(GameObject prefab)
        {
            var recruitedUnit = Instantiate(prefab, UnitSpawnPoint.transform.position, Quaternion.identity);
            recruitedUnit.GetComponent<UnitBase>().FactionOwner = this;

            Units.Add(recruitedUnit);
        }

        /// <summary>
        /// Determines whether this faction has enough resource point to
        /// recruit a unit.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this faction [can recruit unit]; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRecruitUnit(float UnitCost)
        {
            bool canRecruitUnit = UnitCost <= Resource.ResourcePoint;

            if (canRecruitUnit)
            {
                Debug.Log(FactionName + " have enough point to recruit new unit");
            }
            else
            {
                Debug.Log(FactionName + " don't have enough point to recruit new unit");
            }

            return canRecruitUnit;
        }

        /// <summary>
        /// Maintains the unit.
        /// </summary>
        /// <returns>Total cost of maintaining the unit(s).</returns>
        public float MaintainUnit()
        {
            var cost = 0.0f;

            for (int i = Units.Count - 1; i >= 0; i--)
            {
                if (Units[i] == null)
                {
                    Units.RemoveAt(i);
                }
                else
                {
                    var upkeep = Units[i].GetComponent<UnitBase>().Upkeep;
                    Resource.UseResource(upkeep);
                    cost += upkeep;
                }
            }

            return cost;
        }
    }
}