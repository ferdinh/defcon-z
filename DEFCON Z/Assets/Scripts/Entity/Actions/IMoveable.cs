using UnityEngine;

namespace DefconZ.Entity.Action
{
    /// <summary>
    /// Indicates that an object can move.
    /// </summary>
    public interface IMoveable
    {
        /// <summary>
        /// Gets a value indicating whether this object is moving.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is moving; otherwise, <c>false</c>.
        /// </value>
        bool IsMoving { get; }

        /// <summary>
        /// Moves an object to the target position.
        /// </summary>
        /// <param name="target">The target position.</param>
        void MoveTo(Vector3 target);

        /// <summary>
        /// Moves an object to a target object.
        /// </summary>
        /// <param name="targetObj">The target object.</param>
        void MoveTo(GameObject targetObj);

        /// <summary>
        /// Stops the object movement.
        /// </summary>
        void StopMoving();
    } 
}