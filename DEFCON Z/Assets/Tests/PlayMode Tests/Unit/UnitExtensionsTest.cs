using DefconZ;
using DefconZ.Units;
using DefconZ.Utility;
using NUnit.Framework;

namespace Tests.Unit
{
    public class UnitExtensionsTest : BaseTest
    {
        /// <summary>
        /// This test tests whether IsHostile returns true when the two
        /// compared unit is not of the same faction.
        /// </summary>
        [Test]
        public void IsHostile_Should_Return_True_When_Hostile()
        {
            // Arrange
            var humanFaction = _gameObject.AddComponent<HumanFaction>();
            var zombieFaction = _gameObject.AddComponent<ZombieFaction>();

            var humanUnit = _gameObject.AddComponent<Human>();
            humanUnit.FactionOwner = humanFaction;

            var zombieUnit = _gameObject.AddComponent<Zombie>();
            zombieUnit.FactionOwner = zombieFaction;

            var expected = true;

            // Act
            var isHostile = humanUnit.IsHostile(zombieUnit);

            // Assert
            Assert.That(expected, Is.EqualTo(isHostile));
        }

        /// <summary>
        /// This test tests whether IsHostile returns true when the two
        /// compared unit is of the same faction.
        /// </summary>
        [Test]
        public void IsHostile_Should_Return_False_When_NotHostile()
        {
            // Arrange
            var humanFaction = _gameObject.AddComponent<HumanFaction>();

            var humanUnit = _gameObject.AddComponent<Human>();
            humanUnit.FactionOwner = humanFaction;

            var zombieUnit = _gameObject.AddComponent<Zombie>();
            zombieUnit.FactionOwner = humanFaction;

            var expected = false;

            // Act
            var isHostile = humanUnit.IsHostile(zombieUnit);

            // Assert
            Assert.That(expected, Is.EqualTo(isHostile));
        }
    }
}