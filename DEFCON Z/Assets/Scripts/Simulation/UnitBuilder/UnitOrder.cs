using System;
using UnityEngine;

namespace DefconZ.Simulation.UnitBuilder
{
    public class UnitOrder
    {
        public Guid Id { get; } = Guid.NewGuid();
        public GameObject unitPrefab;
        public int recruitTime;
        public Vector3 spawnPoint;

        public UnitOrder(GameObject unitPrefab, Vector3 spawnPoint)
        {
            this.unitPrefab = unitPrefab;
            recruitTime = unitPrefab.GetComponent<UnitBase>().RecruitTime;
            this.spawnPoint = spawnPoint;
        }
    }
}