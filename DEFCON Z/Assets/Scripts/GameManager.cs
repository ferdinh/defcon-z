using DefconZ.Units;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefconZ
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;


        /// <summary>
        /// Holds the faction's information of the game.
        /// </summary>
        private List<Faction> _factions;
        public GameObject _humanPrefab;
        public GameObject _zombiePrefab;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            } else if (Instance != this)
            {
                Destroy(gameObject);
            }

            _factions = new List<Faction>();
        }
        // Start is called before the first frame update
        void Start()
        {
            var humanFaction = gameObject.AddComponent<Faction>();

            humanFaction.UnitPrefab = _humanPrefab;
            humanFaction.FactionType = FactionType.Human;
            humanFaction.FactionName = "Human Player";
            humanFaction.IsHumanPlayer = true;
            var humanUnit = Instantiate(_humanPrefab, new Vector3(-1.90f, 0.0f, -36.0f), Quaternion.identity);
            humanUnit.GetComponent<Human>().FactionOwner = humanFaction.FactionName;

            humanFaction.Units.Add(humanUnit);

            _factions.Add(humanFaction);


            var zombieFaction = gameObject.AddComponent<Faction>();

            zombieFaction.UnitPrefab = _zombiePrefab;
            zombieFaction.FactionType = FactionType.Zombie;
            zombieFaction.FactionName = "Zombie AI";

            var zombieUnit = Instantiate(_zombiePrefab, new Vector3(17.24f, 0.0f, -33.95f), Quaternion.identity);
            zombieUnit.GetComponent<Human>().FactionOwner = zombieFaction.FactionName;



            zombieFaction.Units.Add(zombieUnit);

            _factions.Add(zombieFaction);
