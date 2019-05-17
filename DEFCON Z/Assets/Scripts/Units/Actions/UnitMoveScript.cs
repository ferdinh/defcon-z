using DefconZ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DefconZ.Units.Actions
{
    public class UnitMoveScript : MonoBehaviour, IMoveable
    {
        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        private UnitBase unit;

        // Start is called before the first frame update
        void Start()
        {
            unit = GetComponent<UnitBase>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            if (_navMeshAgent == null)
            {
                Debug.LogError($"Nav Mesh Agent not correctly configured for: {gameObject.name}");
            }
            else
            {
                // First set the target at the current location
                var targetPosition = gameObject.transform.position;
                MoveTo(targetPosition);
            }
        }

        /// <summary>
        /// Move the unit to target position.
        /// </summary>
        /// <param name="target">Target position.</param>
        public void MoveTo(Vector3 target)
        {
            if (target != null)
            {
                _navMeshAgent.SetDestination(target);
            }
        }
    } 
}
