using DefconZ.Units;
﻿using DefconZ.Simulation;
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
        public GameObject HumanPrefab;
        public GameObject ZombiePrefab;
        public IDictionary<Guid, Combat> combats;

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
            combats = new ConcurrentDictionary<Guid, Combat>();
        }

        // Start is called before the first frame update
        void Start()
        {
            var humanFaction = gameObject.AddComponent<Faction>();

            humanFaction.UnitPrefab = HumanPrefab;
            humanFaction.FactionType = FactionType.Human;
            humanFaction.FactionName = "Human Player";
            humanFaction.IsPlayerUnit = true;

            var humanUnit = Instantiate(HumanPrefab, new Vector3(-1.90f, 0.0f, -36.0f), Quaternion.identity);
            humanUnit.GetComponent<Human>().FactionOwner = humanFaction;

            humanFaction.Units.Add(humanUnit);

            Factions.Add(humanFaction);


            var zombieFaction = gameObject.AddComponent<Faction>();

            zombieFaction.UnitPrefab = ZombiePrefab;
            zombieFaction.FactionType = FactionType.Zombie;
            zombieFaction.FactionName = "Zombie AI";

            var zombieUnit = Instantiate(ZombiePrefab, new Vector3(17.24f, 0.0f, -33.95f), Quaternion.identity);
            zombieUnit.GetComponent<Human>().FactionOwner = zombieFaction;



            zombieFaction.Units.Add(zombieUnit);

            Factions.Add(zombieFaction);

            var clock = Clock.Instance;

            clock.GameCycleElapsed += Clock_GameCycleElapsed;
            clock.GameCycleElapsed += Combat;

        }

        /// <summary>
        /// Engage any available active combat.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Combat(object sender, System.EventArgs e)
        {
            foreach (var combat in combats)
            {
                if (combat.Value.IsFighting)
                {
                    combat.Value.Fight();
                }
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