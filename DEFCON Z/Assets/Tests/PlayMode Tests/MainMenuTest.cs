using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
//using UnityEngine.TestTools;
using DefconZ.Simulation;
using DefconZ;
using UnityEngine.UI;

namespace Tests
{
    public class MainMenuTest
    {
        private GameObject MainMenuGameObject = new GameObject();
        private MainMenu MainMenu;

        /// <summary>
        /// Setup test data for each test.
        /// </summary>
        [SetUp]
        public void TestInit()
        {
            MainMenuGameObject = new GameObject();

            // Object is disabled to prevent init code being called.
            MainMenuGameObject.SetActive(false);

            // Set up the components.
            MainMenu = MainMenuGameObject.AddComponent<MainMenu>();
            MainMenu.settingsPanel = new GameObject();
            MainMenu.menuPanel = new GameObject();

            // Reactivate the object to init the components.
            MainMenuGameObject.SetActive(true);
        }

        /// <summary>
        /// Clean up data for each test.
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            MainMenuGameObject = null;
            MainMenu = null;
        }

        /// <summary>
        /// Tests that ensure MainMenu initializes panels state correctly.
        /// </summary>
        [Test]
        public void MainMenu_Should_InitializePanels_Correctly()
        {
            // Arrange
            bool expectedMenuPanelState = true;
            bool expectedSettingsPanelState = false;

            // Act
            // This part has been initiated in the Setup process.

            // Assert
            Assert.That(MainMenu.menuPanel.activeSelf, Is.EqualTo(expectedMenuPanelState));
            Assert.That(MainMenu.settingsPanel.activeSelf, Is.EqualTo(expectedSettingsPanelState));
        }

        public void TestMenuSettingsButtonPanelActive()
        {
            // Arrange 
            MainMenu  testButton= new MainMenu();
            testButton.settingsPanel = new GameObject();
            testButton.menuPanel = new GameObject();
            testButton.settings = false;

            //Act
            testButton.ToggleSettings();
            Assert.That(testButton.settingsPanel.activeSelf, Is.EqualTo(true));
        }

        [Test]
        // checks if quits 
        public void QuitGameTest()
        {
            // Arranges 
            MainMenu mainMenu = new MainMenu();

            // Act
            // press the quit button
            mainMenu.QuitGame();

            UnityEngine.TestTools.LogAssert.Expect(LogType.Log, "Quitting game!");
        }
    }
}
