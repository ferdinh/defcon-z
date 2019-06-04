using DefconZ.Units.Actions;
using System;
using UnityEngine;

namespace DefconZ.Simulation
{
    /// <summary>
    /// Represent a combat between two hostile units.
    /// </summary>
    public class Combat
    {
        public Guid CombatId { get; } = Guid.NewGuid();
        public UnitBase FirstCombatant { get; set; }
        public UnitBase SecondCombatant { get; set; }
        public bool IsFighting { get; set; } = false;

        /// <summary>
        /// Engage each other. The combatant who engage the combat
        /// attacks first.
        /// </summary>
        public void Engage()
        {
            // Only inflict damage when both are fighting,
            // this may be false if unit has not reach within range
            // of each other.
            if (IsFighting)
            {
                // Checks such as range should be here
                // Check that the unit is within range to attack
                if (Vector3.Distance(FirstCombatant.transform.position, SecondCombatant.transform.position) <= FirstCombatant.attackRange)
                {
                    // Make sure both units stop moving
                    FirstCombatant.GetComponent<UnitMoveScript>().StopMoving();
                    SecondCombatant.GetComponent<UnitMoveScript>().StopMoving();

                    // Deal damage to the assigned enemy
                    FirstCombatant.PlayAttackSound();
                    SecondCombatant.TakeDamageFrom(FirstCombatant);
                }
                else
                {
                    // The unit needs to move into range of the defending unit
                    FirstCombatant.GetComponent<UnitMoveScript>().MoveTo(SecondCombatant.transform.position);
                }

                
            }
        }

        /// <summary>
        /// Clears the combat.
        /// </summary>
        public void ClearCombat()
        {
            FirstCombatant.CurrentCombat = null;
            SecondCombatant.CurrentCombat = null;
        }
    }
}