using DefconZ.Simulation;
using DefconZ.Units;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        /// <summary>
        /// Holds the faction's information of the game.
        /// </summary>
        public List<Faction> Factions;

        public IDictionary<Guid, Combat> ActiveCombats;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            Factions = new List<Faction>();
            ActiveCombats = new ConcurrentDictionary<Guid, Combat>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            var humanFaction = gameObject.AddComponent<HumanFaction>();

            humanFaction.UnitPrefab = UnitPrefabList.Instance.Human;
            humanFaction.FactionType = FactionType.Human;
            humanFaction.FactionName = "Human Player";
            humanFaction.IsPlayerUnit = true;

            var humanUnit = Instantiate(humanFaction.UnitPrefab, new Vector3(-1.90f, 0.0f, -36.0f), Quaternion.identity);
            humanUnit.GetComponent<Human>().FactionOwner = humanFaction;

            humanFaction.Units.Add(humanUnit);

            Factions.Add(humanFaction);

            var zombieFaction = gameObject.AddComponent<Faction>();
            var zombieFaction = gameObject.AddComponent<ZombieFaction>();

            zombieFaction.UnitPrefab = UnitPrefabList.Instance.Zombie;
            zombieFaction.FactionType = FactionType.Zombie;
            zombieFaction.FactionName = "Zombie AI";

            var zombieUnit = Instantiate(zombieFaction.UnitPrefab, new Vector3(17.24f, 0.0f, -33.95f), Quaternion.identity);
            var zombieUnit2 = Instantiate(zombieFaction.UnitPrefab, new Vector3(10.24f, 0.0f, -33.95f), Quaternion.identity);
            zombieUnit.GetComponent<Zombie>().FactionOwner = zombieFaction;
            zombieUnit2.GetComponent<Zombie>().FactionOwner = zombieFaction;
            zombieUnit2.GetComponent<Zombie>().objName = "Zombie2";

            zombieFaction.Units.Add(zombieUnit);
            zombieFaction.Units.Add(zombieUnit2);

            Factions.Add(zombieFaction);

            var clock = Clock.Instance;

            clock.GameCycleElapsed += Clock_GameCycleElapsed;
            clock.GameCycleElapsed += Combat;
        }

        /// <summary>
        /// Engage any available combat.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Combat(object sender, System.EventArgs e)
        {
            foreach (var combat in ActiveCombats)
            {
                combat.Value.Engage();
            }
        }

        private void Clock_GameCycleElapsed(object sender, System.EventArgs e)
        {
            foreach (var faction in Factions)
            {
                Debug.Log($"Gathering resource for {faction.FactionName}");
                faction.Resource.GatherResource();
            }

            Debug.Log("Game day elapsed " + Clock.Instance.GameDay);
        }
    }
}