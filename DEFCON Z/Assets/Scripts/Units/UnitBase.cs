using DefconZ.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DefconZ
{
    public abstract class UnitBase : ObjectBase, IDestructible
    {
        public float health;

        private Vector3 targetPosition;
        private NavMeshAgent navMeshAgent;
        public Faction FactionOwner { get; set; }

        public Combat CurrentCombat;
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
            Debug.Log("Moving to:" + target);
            if (target != null)
            {
                navMeshAgent.SetDestination(target);
            }
        }

        /// <summary>
        /// 
        /// <summary>
        /// Initiate an attack to hostile unit.
        /// </summary>
        /// <param name="obj"></param>
        public void StartAttack(GameObject obj)
        {
            // TODO: Check if the target obj is an enemy unit
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
                // Combat status remain inactive until they collided.
                if (CurrentCombat == null)
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
                    var listOfCombat = GameManager.Instance.combats;
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
            health -= damage;

            if (health <= 0.0f)
            {
                DestroySelf();
            }
        }
    }
}