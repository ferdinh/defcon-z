using System.Collections;
using System.Collections.Generic;
using DefconZ;
using DefconZ.UI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class VictoryScreenTest : BaseTest
    {
        /// <summary>
        /// Setup test data for each specific test
        /// </summary>
        [SetUp]
        public void TestInit()
        {
            SceneManager.LoadScene("Defcon city", LoadSceneMode.Single);
        }
        /// <summary>
        /// Cleans up after each test.
        /// </summary>
        public void TestCleanUp()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
        /// <summary>
        /// Tests Victory screen being initialized correctly.
        /// </summary>
        [Test]
        public void VictoryScreen_Should_Be_Initialize_Correctly()
        {
            // Assert
            bool expected = false;

            // Act
            var victoryScreen = _gameObject.AddComponent<VictoryScreen>();

            //Assert
            Assert.That(victoryScreen.gameObject.activeSelf, Is.EqualTo(expected));

        }
        /// Tests whether the victory display gets set to active.
        [UnityTest]
        public IEnumerator DisplayVictory_Should_Set_Object_To_Active()
        {
            bool expected = true;

            var gameManager = GameObject.FindGameObjectWithTag(nameof(GameManager));
            var faction = gameManager.GetComponent<GameManager>().Factions[0];

            var victoryScreen = gameManager.AddComponent<VictoryScreen>();
            victoryScreen.description = gameManager.AddComponent<Text>();
            victoryScreen.winnerLabel = victoryScreen.description;

            // Act 
            victoryScreen.DisplayVictory(faction, faction);

            // Assert 
            Assert.That(victoryScreen.isActiveAndEnabled, Is.EqualTo(expected));

            yield return null;
        }
    }
}
