using DefconZ.Entity.Action;
using UnityEngine;
using UnityEngine.AI;

namespace DefconZ.Units.Actions
{
    /// <summary>
    /// Define the behavior to move a unit.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    /// <seealso cref="DefconZ.Entity.Action.IMoveable" />
    public class UnitMoveScript : MonoBehaviour, IMoveable
    {
        [SerializeField]
        [HideInInspector]
        private NavMeshAgent _navMeshAgent;

        private UnitBase unit;
        public bool log;

        // Start is called before the first frame update
        private void Start()
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

                if (log)
                {
                    Debug.Log($"Moving {unit.objName} to {target}");
                }
            }
        }
    }
}