using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DefconZ
{
    public class UnitBase : ObjectBase
    {
        [SerializeField]
        private Vector3 targetPosition;
        [SerializeField]
        private NavMeshAgent navMeshAgent;

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
        }

        // Update is called once per frame
        public void Update()
        {

        }

        public void DoCurrentAction(Vector3 position)
        {
            Debug.Log(position);
            MoveTo(position);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
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
        /// </summary>
        /// <param name="obj"></param>
        public void StartAttack(GameObject obj)
        {
			// TODO: Check if the target obj is an enemy unit
			UnitBase _targetUnit = obj.GetComponent<UnitBase>();
			if (_targetUnit != null)
			{
				// move to approproate distance
				// TODO: calculate appropriate position to move to (Eg, if this unit is a ranged unit, move to maximum/safe firing range?)
				Vector3 _targetPos = obj.transform.position;
				MoveTo(_targetPos);

				// attack other unit
				// TODO: we need to actully wait until the unit is in attack range before attacking
				// TODO: We need to have a propper damage amount set-up
				Debug.Log("Attacking unit: " + _targetUnit.objName + "\n" + _targetUnit.name);
				_targetUnit.DamageObject(1.0f);
			}


		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage"></param>
        public override void DamageObject(float damage)
        {
            if (destructible)
            {
                health -= damage;

                if (health <= 0.0f)
                {
                    DestroySelf();
                }
            }
        }

        public override void DestroySelf()
        {
            Debug.Log(this.objName + " has reached 0 or less health and has been destroyed");
            Destroy(gameObject); // Remove the game object this script is attached to
        }
    }
}