using DefconZ.Entity.Action;
using DefconZ.GameLevel;
using DefconZ.Units;
using UnityEngine;

namespace DefconZ.Simulation
{
    public class AI : MonoBehaviour
    {
        public Faction playerFaction;
        public GameManager gameManager;

        /// <summary>
        /// Runs the specified enemy faction.
        /// </summary>
        /// <param name="enemyFaction">The enemy faction.</param>
        public void Run(Faction enemyFaction)
        {
            enemyFaction.RecruitUnitAt(enemyFaction.UnitSpawnPoint.transform.position);
            
            foreach (var unitObj in enemyFaction.Units)
            {
                var unit = unitObj.GetComponent<UnitBase>();

                if (!unit.CombatPresent())
                {
                    var unitMoveScript = unitObj.GetComponent<UnitBase>().GetComponent<IMoveable>();

                    if (!unitMoveScript.IsMoving)
                    {
                        unitMoveScript.MoveTo(GetAttackPoint(unit));
                    }
                }
            }
        }

        public Vector3 GetAttackPoint(UnitBase unit)
        {
            Vector3 attackPoint = unit.FactionOwner.UnitSpawnPoint.transform.position;
            float distance = -1;

            foreach (Zone zone in gameManager.zoneManager.managedZones)
            {
                if (zone.zoneOwner != unit.FactionOwner)
                {
                    float distanceToZone = Vector3.Distance(unit.transform.position, zone.transform.position);
                    if (distanceToZone < distance || distance == -1)
                    {
                        distance = distanceToZone;
                        attackPoint = zone.transform.position;
                    }
                }
            }

            return attackPoint;
        }
    }
}