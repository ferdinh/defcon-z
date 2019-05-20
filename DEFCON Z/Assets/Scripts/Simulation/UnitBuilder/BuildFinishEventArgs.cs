using System;
using UnityEngine;

namespace DefconZ.Simulation.UnitBuilder
{
    public class BuildFinishedEventArgs : EventArgs
    {
        public GameObject createdUnit;

        public BuildFinishedEventArgs(GameObject unitCreated)
        {
            createdUnit = unitCreated;
        }
    }
}