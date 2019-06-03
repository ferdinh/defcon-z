using DefconZ.Simulation;
using DefconZ.UI;
using DefconZ.Units;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefconZ
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Holds the faction's information of the game.
        /// </summary>
        public List<Faction> Factions;

        public IDictionary<Guid, Combat> ActiveCombats;

        public IList<Events> listOfEvents;

        private Clock _clock;
        private AI _AI;

        private bool gameOver = false;

        private void Awake()
        {
            _clock = gameObject.AddComponent<Clock>();
            Factions = new List<Faction>();
            ActiveCombats = new ConcurrentDictionary<Guid, Combat>();
            _AI = gameObject.AddComponent<AI>();
            listOfEvents = new List<Events>
            {
                new Events
                {
                    name = "Abundance",
                    modifier = new Modifier
                    {
                        Name = "Abundance",
                        Value = 1.0f
                    }
                }
            };
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

            _clock.GameCycleElapsed += Clock_GameCycleElapsed;
            _clock.GameCycleElapsed += Combat;
            _clock.GameCycleElapsed += VictoryCheck;

            // Once the GameManager has finished initialising, tell the in-game UI to initialise
            InGameUI inGameUI = GameObject.Find("InGameUI").GetComponent<InGameUI>();
            inGameUI.InitUI(humanFaction);
            inGameUI.PostInitUI();

            if (Difficulty.SelectedDifficulty != null)
            {
                SetDifficulty(Difficulty.SelectedDifficulty);
            }
            else
            {
                // If a difficulty has not been selected, default to normal difficulty
                SetDifficulty(Difficulty.Normal);
            }
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

                Debug.Log($"{faction.FactionName} has {faction.Resource.ResourcePoint} amount of resources out of {faction.Resource.GetMaxResourcePoint}.");

                if (!faction.IsPlayerUnit)
                {
                    _AI.Run(faction);
                }

                var abundanceEvent = listOfEvents.SingleOrDefault(events => events.name.Equals("Abundance"));

                if (faction.Resource.OwnedZones.Count > 5 && !faction.Modifiers.Any(mod => mod.Name.Equals("Abundance")))
                {
                    faction.Modifiers.Add(abundanceEvent.modifier);
                    Debug.LogError("Abundance Event Modifier Added.");
                }

                if (faction.Resource.OwnedZones.Count < 5 && faction.Modifiers.Any(mod => mod.Name.Equals("Abundance")))
                {
                    faction.Modifiers.Remove(abundanceEvent.modifier);
                    Debug.LogError("Abundance Event Modifier Removed.");
                }
            }

            Debug.Log("Game day elapsed " + _clock.GameDay);
        }

        /// <summary>
        /// Checks if any faction has won or lost the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VictoryCheck(object sender, System.EventArgs e)
        {
            if (!gameOver)
            {
                Faction loser = null;
                Faction winner = null;

                // Check for faction victory
                foreach (Faction faction in Factions)
                {
                    // Check if each faction satisfies a loose condition.
                    if (faction.Units.Count <= 0 || faction.Resource.OwnedZones.Count <= 0)
                    {
                        loser = faction;
                    }
                    else
                    {
                        winner = faction;
                    }
                }

                if (loser != null)
                {
                    Debug.Log($"{loser.FactionName} has lost the game!");
                    Debug.Log($"{winner.FactionName} has won the game!");
                    Player player = GameObject.Find("Player").GetComponent<Player>();
                    player.inGameUI.EndGameScreen(loser, winner);
                    gameOver = true;
                }
            }
        }

        /// <summary>
        /// Sets the game difficulty.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        public void SetDifficulty(Modifier difficulty)
        {
            foreach (var faction in Factions)
            {
                var modValue = difficulty.Value;

                // If it is not player's faction, negate the modifier value
                // so it has an opposite effect. For example on easy
                // difficulty with modifier value of 0.5f will make player's
                // faction stronger but will cripple the AI's Faction.
                if (!faction.IsPlayerUnit)
                {
                    modValue *= -1;
                }

                faction.Difficulty.Name = difficulty.Name;
                faction.Difficulty.Type = difficulty.Type;
                faction.Difficulty.Value = modValue;
            }
        }

        /// <summary>
        /// Removes a unit from active state before deleting.
        /// </summary>
        /// <param name="unit"></param>
        public void RemoveUnit(UnitBase unit, float delay)
        {
            GameObject unitGameObject = unit.gameObject;
            // Remove the unit gamescript from the unit
            Destroy(unit);

            Destroy(unitGameObject, delay);
        }
    }
}