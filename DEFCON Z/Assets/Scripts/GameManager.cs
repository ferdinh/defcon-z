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

            var zombieFaction = gameObject.AddComponent<ZombieFaction>();

            zombieFaction.UnitPrefab = UnitPrefabList.Instance.Zombie;
            zombieFaction.FactionType = FactionType.Zombie;
            zombieFaction.FactionName = "Zombie AI";

            Factions.Add(humanFaction);
            Factions.Add(zombieFaction);

            humanFaction.RecruitUnit();
            zombieFaction.RecruitUnit();

            var clock = Clock.Instance;

            clock.GameCycleElapsed += Clock_GameCycleElapsed;
            clock.GameCycleElapsed += Combat;

            // Once the GameManager has finished initialising, tell the in-game UI to initialise
            GameObject.Find("UI").GetComponent<InGameUI>().InitUI(humanFaction);
            SetDifficulty(Difficulty.SelectDifficulty);
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

        /// <summary>
        /// Occurs when a game day has passed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Clock_GameCycleElapsed(object sender, System.EventArgs e)
        {
            foreach (var faction in Factions)
            {
                Debug.Log($"Gathering resource for {faction.FactionName}");
                var resourceGathered = faction.Resource.GatherResource();
                var maintenanceCost = faction.MaintainUnit();

                Debug.Log($"Gathered {resourceGathered} amount of resource.");
                Debug.Log($"Maintenance cost at {maintenanceCost}");

                Debug.Log($"{faction.FactionName} has {faction.Resource.ResourcePoint} amount of resources.");
            }

            Debug.Log("Game day elapsed " + Clock.Instance.GameDay);
        }

        /// <summary>
        /// Sets the game difficulty.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        public void SetDifficulty(Modifier difficulty)
        {
            foreach (var faction in Factions)
            {
                faction.Difficulty.Name = difficulty.Name;
                faction.Difficulty.Type = difficulty.Type;
                faction.Difficulty.Value = difficulty.Value;
            }
        }
    }
}