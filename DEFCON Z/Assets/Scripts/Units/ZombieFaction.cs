using UnityEngine;

namespace DefconZ.Units
{
    public class ZombieFaction : Faction
    {
        protected override void InitAwake()
        {
            base.InitAwake();

            UnitSpawnPoint = GameObject.Find("ZombieSpawnPoint");
        }
    }
}