using System;

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
                var damageToFirstCombatant = SecondCombatant.CalculateDamage();
                var damageToSecondCombatant = FirstCombatant.CalculateDamage();

                SecondCombatant.TakeDamage(damageToSecondCombatant);

                if (SecondCombatant.health <= 0)
                {
                    damageToFirstCombatant = 0.0f;
                }

                FirstCombatant.TakeDamage(damageToFirstCombatant);
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