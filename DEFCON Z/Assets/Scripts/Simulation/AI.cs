using DefconZ.Entity.Action;
using DefconZ.Units;
using UnityEngine;

namespace DefconZ.Simulation
{
    public class AI : MonoBehaviour
    {
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

                if (unit.CurrentCombat == null)
                {
                    var unitMoveScript = unitObj.GetComponent<UnitBase>().GetComponent<IMoveable>();

                    if (!unitMoveScript.IsMoving)
                    {
                        unitMoveScript.MoveTo(new Vector3(10, 0, 20));
                    }
                }
                else
                {
                    if (!unit.CurrentCombat.IsFighting)
                    {
                        var unitMoveScript = unitObj.GetComponent<UnitBase>().GetComponent<IMoveable>();
                        unitMoveScript.MoveTo(unit.CurrentCombat.FirstCombatant.gameObject);
                    }
                }
            }
        }
    }
}