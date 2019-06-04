using DefconZ;
using DefconZ.Simulation;
using DefconZ.Units;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    /// <summary>
    /// Test suite for Combat
    /// </summary>
    public class CombatTest
    {
        [Test]
        public void Combat_Engage_When_IsFighting()
        {
            // Arrange
            var gameObject = new GameObject();

            var firstCombatant = gameObject.AddComponent<Human>();
            var secondCombatant = gameObject.AddComponent<Zombie>();

            /*var combat = new Combat
            {
                FirstCombatant = firstCombatant,
                SecondCombatant = secondCombatant,
                IsFighting = true
            };

            // Act
            combat.Engage();
            */
            // Assert
            Assert.That(firstCombatant.health, Is.LessThan(100.0f));
            Assert.That(secondCombatant.health, Is.LessThan(100.0f));
        }

        [Test]
        public void Combat_ShouldNot_Engage_When_IsNotFighting()
        {
            // Arrange
            var gameObject = new GameObject();

            var firstCombatant = gameObject.AddComponent<Human>();
            var secondCombatant = gameObject.AddComponent<Zombie>();

            var firstCombatantStartingHealth = firstCombatant.health;
            var secondCombatantStartingHealth = secondCombatant.health;
            /*
            var combat = new Combat
            {
                FirstCombatant = firstCombatant,
                SecondCombatant = secondCombatant,
            };

            float margin = 0.001f;

            // Act
            combat.Engage();
            
            // Assert
            Assert.That(firstCombatant.health, Is.EqualTo(firstCombatantStartingHealth).Within(margin));
            Assert.That(secondCombatant.health, Is.EqualTo(secondCombatantStartingHealth).Within(margin));
            */
        }
    }
}