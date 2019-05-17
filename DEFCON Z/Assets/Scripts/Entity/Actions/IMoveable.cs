using UnityEngine;

namespace DefconZ.Entity.Action
{
    /// <summary>
    /// Indicates that an object can move.
    /// </summary>
    public interface IMoveable
    {
        /// <summary>
        /// Moves an object to the target position.
        /// </summary>
        /// <param name="target">The target.</param>
        void MoveTo(Vector3 target);
    } 
}