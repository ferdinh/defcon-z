using UnityEngine;

namespace DefconZ.Units
{
    public class ZombieFaction : Faction
    {
        protected override void InitStart()
        {
            base.InitStart();

            UnitSpawnPoint = GameObject.Find("ZombieSpawnPoint");
        }
    }
}