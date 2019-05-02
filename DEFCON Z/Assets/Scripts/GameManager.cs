using DefconZ.Units;
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
        }

        // Start is called before the first frame update
        void Start()
        {
            var humanFaction = gameObject.AddComponent<Faction>();

            humanFaction.UnitPrefab = HumanPrefab;
            humanFaction.FactionType = FactionType.Human;
            humanFaction.FactionName = "Human Player";
            humanFaction.IsHumanPlayer = true;

            var humanUnit = Instantiate(HumanPrefab, new Vector3(-1.90f, 0.0f, -36.0f), Quaternion.identity);
            humanUnit.GetComponent<Human>().FactionOwner = humanFaction.FactionName;

            humanFaction.Units.Add(humanUnit);

            Factions.Add(humanFaction);


            var zombieFaction = gameObject.AddComponent<Faction>();

            zombieFaction.UnitPrefab = ZombiePrefab;
            zombieFaction.FactionType = FactionType.Zombie;
            zombieFaction.FactionName = "Zombie AI";

            var zombieUnit = Instantiate(ZombiePrefab, new Vector3(17.24f, 0.0f, -33.95f), Quaternion.identity);
            zombieUnit.GetComponent<Human>().FactionOwner = zombieFaction.FactionName;



            zombieFaction.Units.Add(zombieUnit);

            Factions.Add(zombieFaction);

            var clock = Clock.Instance;

            clock.GameCycleElapsed += Clock_GameCycleElapsed;

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