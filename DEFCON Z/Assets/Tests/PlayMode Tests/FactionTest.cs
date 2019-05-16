using DefconZ;
using DefconZ.Units;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class FactionTest : BaseTest
    {
        private Faction Faction;

        /// <summary>
        /// Setup test data for each test.
        /// </summary>
        [SetUp]
        public void TestInit()
        {
            Faction = _gameObject.AddComponent<Faction>();
        }

        /// <summary>
        /// Clean up after each test.
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            Faction = null;
        }

        /// <summary>
        /// Tests whether the method can determine if there is enough resources
        /// to recruit unit
        /// </summary>
        [Test]
        public void CanRecruitUnit_Should_Return_True_When_ThereIsEnoughResourceToRecruit()
        {
            // Arrange
            bool expected = true;

            // Using a low number of unit cost to ensure that faction have
            // enough resource to recruit one hypothetical unit.
            float unitCost = 1.0f;

            // Act
            bool actual = Faction.CanRecruitUnit(unitCost);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests whether the method can determine if there is enough resources
        /// to recruit unit
        /// </summary>
        [Test]
        public void CanRecruitUnit_Should_Return_False_When_ThereIsNotEnoughResourceToRecruit()
        {
            // Arrange
            bool expected = false;

            // Using a high number of unit cost to ensure that faction don't
            // have enough resource to recruit one hypothetical unit.
            float unitCost = 10000000.0f;

            // Act
            bool actual = Faction.CanRecruitUnit(unitCost);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// This test whether unit maintenance calculation is correct.
        /// </summary>
        [Test]
        public void MaintainUnit_Should_Consume_Resource_BasedOnNumberOfUnits()
        {
            // Arrange
            float baseUpkeep = 100.0f;

            // Create two units each
            var zombie = new GameObject();
            zombie.AddComponent<Zombie>();
            zombie.GetComponent<UnitBase>().Upkeep = baseUpkeep;

            var human = new GameObject();
            human.AddComponent<Human>();
            human.GetComponent<UnitBase>().Upkeep = baseUpkeep;

            Faction.Units.Add(zombie);
            Faction.Units.Add(human);

            float startResource = Faction.Resource.ResourcePoint;

            // Expected end resource is dependent on how many units there are,
            // upkeep of each unit is sum together.
            float expectedEndResource = startResource - (baseUpkeep * Faction.Units.Count);

            // Act
            Faction.MaintainUnit();

            // Assert
            Assert.That(Faction.Resource.ResourcePoint, Is.EqualTo(expectedEndResource));
        }
    }
}