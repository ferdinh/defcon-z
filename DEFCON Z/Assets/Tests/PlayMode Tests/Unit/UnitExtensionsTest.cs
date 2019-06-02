using DefconZ;
using DefconZ.Units;
using DefconZ.Utility;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.Unit
{
    public class UnitExtensionsTest : BaseTest
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
        /// This test tests whether IsHostile returns true when the two
        /// compared unit is not of the same faction.
        /// </summary>
        [UnityTest]
        public IEnumerator IsHostile_Should_Return_True_When_Hostile()
        {
            yield return null;

            // Arrange
            var factions = getFaction();
            var faction1Unit = factions.faction1.Units[0].GetComponent<UnitBase>();
            var faction2Unit = factions.faction2.Units[0].GetComponent<UnitBase>();

            var expected = true;

            // Act
            var isHostile = faction1Unit.IsHostile(faction2Unit);

            // Assert
            Assert.That(isHostile, Is.EqualTo(expected));
        }

        /// <summary>
        /// This test tests whether IsHostile returns true when the two
        /// compared unit is of the same faction.
        /// </summary>
        [UnityTest]
        public IEnumerator IsHostile_Should_Return_False_When_NotHostile()
        {
            yield return null;

            // Arrange
            var factions = getFaction();
            var faction1Unit = factions.faction1.Units[0].GetComponent<UnitBase>();
            var expected = false;

            // Act
            var isHostile = faction1Unit.IsHostile(faction1Unit);

            // Assert
            Assert.That(isHostile, Is.EqualTo(expected));
        }

        /// <summary>
        /// Gets the faction from the game manager.
        /// </summary>
        /// <returns></returns>
        private (Faction faction1, Faction faction2) getFaction()
        {
            var gameManager = GameObject.FindGameObjectWithTag(nameof(GameManager));
            var faction1 = gameManager.GetComponent<GameManager>().Factions[0];
            var faction2 = gameManager.GetComponent<GameManager>().Factions[1];

            return (faction1, faction2);
        }
    }
}