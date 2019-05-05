using UnityEngine;

namespace DefconZ.Units
{
    public class HumanFaction : Faction
    {
        protected override void InitStart()
        {
            base.InitStart();

            UnitSpawnPoint = GameObject.Find("HumanSpawnPoint");
        }
    }
}