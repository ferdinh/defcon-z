using DefconZ.Entity.Action;
using System;
using System.Collections;
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

        private object _lock = new object();

        private UnitBase unit;
        public bool log;
        private IEnumerator _checkIfReachDestinationCoroutine;

        /// <summary>
        /// Gets a value indicating whether this object is moving.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is moving; otherwise, <c>false</c>.
        /// </value>
        public bool IsMoving { get; private set; }

        // Start is called before the first frame update
        private void Start()
        {
            unit = GetComponent<UnitBase>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _checkIfReachDestinationCoroutine = CheckIfReachDestination();

            if (_navMeshAgent == null)
            {
                Debug.LogError($"Nav Mesh Agent not correctly configured for: {gameObject.name}");
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
                if (_navMeshAgent != null)
                {
                    StopCoroutine(_checkIfReachDestinationCoroutine);

                    _navMeshAgent.ResetPath();

                    _navMeshAgent.SetDestination(target);
                    IsMoving = true;

                    StartCoroutine(_checkIfReachDestinationCoroutine);

                    if (log)
                    {
                        Debug.Log($"Moving {unit.objName} to {target}");
                    }
                }
            }
        }

        /// <summary>
        /// Moves an object to a target object.
        /// </summary>
        /// <param name="targetObj">The target object.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void MoveTo(GameObject targetObj)
        {
            IsMoving = true;
            StartCoroutine(MovingToTarget(targetObj));
        }

        /// <summary>
        /// Update destination with moving to a target object.
        /// </summary>
        /// <param name="targetObj">The target object.</param>
        /// <returns></returns>
        private IEnumerator MovingToTarget(GameObject targetObj)
        {
            if (!IsMoving)
            {
                StopCoroutine(MovingToTarget(targetObj));
            }

            while (IsMoving)
            {
                if (targetObj != null)
                {
                    _navMeshAgent.SetDestination(targetObj.transform.position);
                    yield return null;
                }
                else
                {
                    StopMoving();
                }
            }
        }

        /// <summary>
        /// Checks if the unit has reach its destination.
        /// </summary>
        /// <returns></returns>
        private IEnumerator CheckIfReachDestination()
        {
            while (true)
            {
                yield return new WaitUntil(() => !_navMeshAgent.pathPending);
                float remainingDestination = _navMeshAgent.remainingDistance;

                if (remainingDestination != Mathf.Infinity && _navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && _navMeshAgent.remainingDistance == 0)
                {
                    lock (_lock)
                    {
                        IsMoving = false;

                        Debug.Log(gameObject.name + " reached destination");

                        StopCoroutine(_checkIfReachDestinationCoroutine);
                    }
                }

                yield return null;
            }
        }

        /// <summary>
        /// Stops the object movement.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void StopMoving()
        {
            lock (_lock)
            {
                IsMoving = false;
            }
            _navMeshAgent.ResetPath();
            StopCoroutine(_checkIfReachDestinationCoroutine);
        }
    }
}