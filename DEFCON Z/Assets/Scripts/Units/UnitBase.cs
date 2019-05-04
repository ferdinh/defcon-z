using DefconZ.Simulation;
using DefconZ.Units;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace DefconZ
{
    public abstract class UnitBase : ObjectBase, IDestructible
    {
        public float health = 100;

        private Vector3 targetPosition;
        private NavMeshAgent navMeshAgent;
        public Faction FactionOwner { get; set; }

        public Combat CurrentCombat;

        /*
         * All unit has an initial base health of 100 and base damage of 10.
         * The most barebone/basic unit takes 10x hit to be defeated.
         *
         */
        protected float baseHealth = 100.0f;
        protected float baseDamage = 10.0f;
        public IList<Modifier> Modifiers = new List<Modifier>();

        // Start is called before the first frame update
        public void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

            // check if the nav mesh exists
            if (navMeshAgent == null)
            {
                Debug.Log("Nav Mesh Agent not correctly configured for: " + gameObject.name);
            }
            else
            {
                // First set the target as the current location
                targetPosition = gameObject.transform.position;
                MoveTo(targetPosition);
            }

            InitUnit();
        }

        /// <summary>
        /// Initialises the unit, called upon the first frame the unit is instantiated
        /// </summary>
        public abstract void InitUnit();

        public abstract void Update();

        public void DoCurrentAction(Vector3 position)
        {
            Debug.Log(position);
            MoveTo(position);
        }

        /// <summary>
        /// Move the unit to target position.
        /// </summary>
        /// <param name="target">Target position.</param>
        public void MoveTo(Vector3 target)
        {
            //if (CombatPresent())
            //{
            //    GameManager.Instance.ActiveCombats.Remove(CurrentCombat.CombatId);
            //    CurrentCombat.ClearCombat();
            //}

            Debug.Log("Moving to:" + target);
            if (target != null)
            {
                navMeshAgent.SetDestination(target);
            }
        }

        /// <summary>
        /// Called when unit collider collided with each other.
        /// </summary>
        /// <param name="other">The other collider.</param>
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger entered");
            // We expect unit to collide with another unit.
            var collidedGameObject = other.gameObject.GetComponent<UnitBase>();

            if (collidedGameObject != null)
            {
                // Engage combat only if the unit collides with a hostile unit.
                if (collidedGameObject.FactionOwner != this.FactionOwner)
                {
                    if (CombatPresent())
                    {
                        lock (CurrentCombat)
                        {
                            if (CurrentCombat != null)
                            {
                                CurrentCombat.IsFighting = true;
                                Debug.Log("Collided with an enemy unit and starts engaging");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called when two collider move away from each other.
        /// </summary>
        /// <param name="other">The other collider.</param>
        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Trigger exit " + this.objName);
            // We expect unit to depart from another unit.
            var collidedGameObject = other.gameObject.GetComponent<UnitBase>();

            if (collidedGameObject != null)
            {
                // Remove combat only if it is from another hostile unit.
                if (collidedGameObject.FactionOwner != this.FactionOwner)
                {
                    if (CurrentCombat != null)
                    {
                        if (GameManager.Instance.ActiveCombats.Remove(CurrentCombat.CombatId))
                        {
                            Debug.Log($"Combat with {CurrentCombat.CombatId} removed");
                        }
                    }

                    // Since combat no longer applicable, remove it.
                    this.CurrentCombat = null;
                }
            }
        }

        /// <summary>
        /// Initiate an attack to hostile unit.
        /// </summary>
        /// <param name="obj"></param>
        public void StartAttack(GameObject obj)
        {
            UnitBase _targetUnit = obj.GetComponent<UnitBase>();
            if (_targetUnit != null)
            {
                // move to appropriate distance
                // TODO: calculate appropriate position to move to (Eg, if this unit is a ranged unit, move to maximum/safe firing range?)
                Vector3 _targetPos = obj.transform.position;
                MoveTo(_targetPos);

                // attack other unit
                Debug.Log("Attacking unit: " + _targetUnit.objName + "\n" + _targetUnit.name);

                // If there is no current combat for the unit,
                // create a new one.
                // Combat status remain inactive until they collides.
                if (!CombatPresent())
                {
                    // Create new combat
                    var combat = new Combat
                    {
                        FirstCombatant = this,
                        SecondCombatant = _targetUnit
                    };

                    CurrentCombat = combat;

                    // Set the target unit combat.
                    // Both this unit and target unit should have the same
                    // combat.
                    _targetUnit.CurrentCombat = CurrentCombat;

                    // Register the combat to the game manager.
                    var listOfCombat = GameManager.Instance.ActiveCombats;
                    listOfCombat.Add(CurrentCombat.CombatId, CurrentCombat);

                    Debug.Log($"Created new Combat with id {CurrentCombat.CombatId}");
                }
            }
        }

        /// <summary>
        /// Defines how a unit is destroyed
        /// </summary>
        public virtual void DestroySelf()
        {
            Debug.Log(this.objName + " has reached 0 or less health and has been destroyed");
            Destroy(gameObject); // Remove the game object this script is attached to
        }

        /// <summary>
        /// Defines how a unit takes damage
        /// If the remaining health is less than or equal to zero, the unit is destroyed
        /// </summary>
        /// <param name="damage"></param>
        public virtual void TakeDamage(float damage)
        {
            Debug.Log(this.objName + " " + this.health);

            health -= damage;

            // When a unit dies, remove the combat from list of combats and
            // remove the combat from the winning unit whilst destroy the
            // losing unit.
            if (health <= 0.0f)
            {
                if (CombatPresent())
                {
                    GameManager.Instance.ActiveCombats.Remove(CurrentCombat.CombatId);
                    CurrentCombat.ClearCombat();
                }

                DestroySelf();
            }
        }

        /// <summary>
        /// Calculates the damage to be inflicted.
        /// </summary>
        /// <returns>Damage given.</returns>
        public float CalculateDamage()
        {
            // To adjust different value in a unit,
            // add more modifier to derived classes
            float baseModifier = 1.0f;
            float attackModifier = baseModifier + Modifiers.Sum(mod => mod.Value);

            float variableMultiplier = UnityEngine.Random.Range(0.05f, 0.1f);

            return baseDamage * (attackModifier + variableMultiplier);
        }

        /// <summary>
        /// Checks if this instance currently have a combat.
        /// </summary>
        /// <returns>True if combat is present, else, false.</returns>
        private bool CombatPresent()
        {
            return CurrentCombat != null;
        }

        private static bool RemoveCombat(Combat combatToRemove)
        {
            var removeResult = GameManager.Instance.ActiveCombats.Remove(combatToRemove.CombatId);
            combatToRemove.ClearCombat();

            return removeResult;
        }
    }
}