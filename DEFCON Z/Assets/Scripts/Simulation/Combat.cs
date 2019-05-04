using System;

namespace DefconZ.Simulation
{
    public class Combat
    {
        public Guid CombatId { get; } = Guid.NewGuid();
        public UnitBase FirstCombatant { get; set; }
        public UnitBase SecondCombatant { get; set; }
        public bool IsFighting { get; set; } = false;


        public void Fight()
        {
            var damageToFirstCombatant = SecondCombatant.CalculateDamage();
            var damageToSecondCombatant = FirstCombatant.CalculateDamage();

            FirstCombatant.TakeDamage(damageToFirstCombatant);
            SecondCombatant.TakeDamage(damageToSecondCombatant);
        }
    }
}