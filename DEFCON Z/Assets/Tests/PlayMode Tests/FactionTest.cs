using DefconZ;
using DefconZ.Units;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class FactionTest : BaseTest
    {
        /// <summary>
        /// Setup test data for each test.
        /// </summary>
        [SetUp]
        public void TestInit()
        {
            SceneManager.LoadScene("defcon city test", LoadSceneMode.Single);
        }

        /// <summary>
        /// Clean up after each test.
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }

        /// <summary>
        /// Tests whether the method can determine if there is enough resources
        /// to recruit unit
        /// </summary>
        [UnityTest]
        public IEnumerator CanRecruitUnit_Should_Return_True_When_ThereIsEnoughResourceToRecruit()
        {
            yield return null;

            // Arrange
            bool expected = true;
            var faction = getFaction();

            // Using a low number of unit cost to ensure that faction have
            // enough resource to recruit one hypothetical unit.
            float unitCost = 1.0f;

            // Act
            bool actual = faction.CanRecruitUnit(unitCost);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests whether the method can determine if there is enough resources
        /// to recruit unit
        /// </summary>
        [UnityTest]
        public IEnumerator CanRecruitUnit_Should_Return_False_When_ThereIsNotEnoughResourceToRecruit()
        {
            yield return null;

            // Arrange
            bool expected = false;
            var faction = getFaction();

            // Using a high number of unit cost to ensure that faction don't
            // have enough resource to recruit one hypothetical unit.
            float unitCost = 10000000.0f;

            // Act
            bool actual = faction.CanRecruitUnit(unitCost);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// This test whether unit maintenance calculation is correct.
        /// </summary>
        [UnityTest]
        public IEnumerator MaintainUnit_Should_Consume_Resource_BasedOnNumberOfUnits()
        {
            yield return null;

            // Arrange
            float baseUpkeep = 100.0f;
            var faction = getFaction();

            // Clear any units in the faction.
            faction.Units.Clear();

            // Create two units each
            var zombie = new GameObject();
            zombie.AddComponent<Zombie>();
            zombie.GetComponent<UnitBase>().Upkeep = baseUpkeep;

            var human = new GameObject();
            human.AddComponent<Human>();
            human.GetComponent<UnitBase>().Upkeep = baseUpkeep;

            faction.Units.Add(zombie);
            faction.Units.Add(human);

            float startResource = faction.Resource.ResourcePoint;

            // Expected end resource is dependent on how many units there are,
            // upkeep of each unit is sum together.
            float expectedEndResource = startResource - (baseUpkeep * faction.Units.Count);

            // Act
            faction.MaintainUnit();

            // Assert
            Assert.That(faction.Resource.ResourcePoint, Is.EqualTo(expectedEndResource));
        }

        /// <summary>
        /// This tests that MaintainUnit should remove destroyed/dead unit from
        /// faction's list.
        /// </summary>
        [UnityTest]
        public IEnumerator MaintainUnit_Should_Remove_Destroyed_Unit_From_List()
        {
            yield return null;

            // Arrange
            int maxUnitToGenerate = 10;
            var faction = getFaction();

            // Clear any units in the faction.
            faction.Units.Clear();

            for (int i = 0; i < maxUnitToGenerate; i++)
            {
                var unit = new GameObject();
                unit.AddComponent<Human>();

                faction.Units.Add(unit);
            }

            // Act
            // Destroy units that is even in the index position
            for (int i = 0; i < maxUnitToGenerate; i++)
            {
                if (i % 2 == 0)
                {
                    Object.DestroyImmediate(faction.Units[i].gameObject);
                }
            }

            faction.MaintainUnit();

            // Assert
            Assert.That(faction.Units.Count, Is.EqualTo(maxUnitToGenerate / 2));
        }

        /// <summary>
        /// This test ensure that MaintainUnit does not throw null when accessing
        /// 'Unit' object.
        /// </summary>
        [UnityTest]
        public IEnumerator MaintainUnit_Should_Not_Throw_Null()
        {
            yield return null;

            // Arrange
            var faction = getFaction();
            var unit = new GameObject();
            unit.AddComponent<Human>();

            faction.Units.Add(unit);

            // Act
            // Destroy units that is even in the index position
            Object.DestroyImmediate(faction.Units[0].gameObject);

            // Assert
            Assert.DoesNotThrow(() => faction.MaintainUnit());
        }

        /// <summary>
        /// Gets the faction from the game manager.
        /// </summary>
        /// <returns></returns>
        private Faction getFaction()
        {
            var gameManager = GameObject.FindGameObjectWithTag(nameof(GameManager));
            var faction = gameManager.GetComponent<GameManager>().Factions[0];

            return faction;
        }
    }
}