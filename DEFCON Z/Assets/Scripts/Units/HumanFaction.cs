using UnityEngine;

namespace DefconZ.Units
{
    public class HumanFaction : Faction
    {
        protected override void InitAwake()
        {
            base.InitAwake();

            UnitSpawnPoint = GameObject.Find("HumanSpawnPoint");
        }
    }
}