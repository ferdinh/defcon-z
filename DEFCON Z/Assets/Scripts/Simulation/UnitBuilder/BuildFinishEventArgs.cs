using System;
using UnityEngine;

namespace DefconZ.Simulation.UnitBuilder
{
    /// <summary>
    /// Event data after <see cref="UnitOrder"/> has been completed.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class BuildFinishedEventArgs : EventArgs
    {
        public GameObject createdUnit;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildFinishedEventArgs"/> class.
        /// </summary>
        /// <param name="unitCreated">The unit created.</param>
        public BuildFinishedEventArgs(GameObject unitCreated)
        {
            createdUnit = unitCreated;
        }
    }
}