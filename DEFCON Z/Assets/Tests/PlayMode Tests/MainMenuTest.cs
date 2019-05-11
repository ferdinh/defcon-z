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

        /// <summary>
        /// Test ensures that main menu toggle as it should.
        /// </summary>
        /// <param name="initialSettingsState">Initial settings bool.</param>
        /// <param name="expectedMenuPanelState">Expected Menu Panel state after toggling.</param>
        /// <param name="expectedSettingsPanelState">Expected Settings Panel state after toggling.</param>
        /// <param name="expectedSettingsState">Expected Main Menu settings state after toggling.</param>
        [TestCase(false, false, true, true)]
        [TestCase(true, true, false, false)]
        public void MainMenu_Should_ToggleSettings(bool initialSettingsState, bool expectedMenuPanelState, bool expectedSettingsPanelState, bool expectedSettingsState)
        {
            // Arrange
            MainMenu.settings = initialSettingsState;

            // Act
            MainMenu.ToggleSettings();

            // Assert
            Debug.Log($"Initial setting state: {initialSettingsState}");

            // Assert Menu Panel state.
            bool actualMenuPanelState = MainMenu.menuPanel.activeSelf;
            Debug.Log($"Menu panel state: {actualMenuPanelState}, expected: {expectedMenuPanelState}");
            Assert.That(actualMenuPanelState, Is.EqualTo(expectedMenuPanelState));

            // Assert Settings Panel state.
            bool actualSettingsPanelState = MainMenu.settingsPanel.activeSelf;
            Debug.Log($"Settings panel state: {actualSettingsPanelState}, expected: {expectedSettingsPanelState}");
            Assert.That(actualSettingsPanelState, Is.EqualTo(expectedSettingsPanelState));

            // Assert Settings state.
            bool actualSettingsState = MainMenu.settings;
            Debug.Log($"Settings panel state: {actualSettingsState}, expected: {expectedSettingsState}");
            Assert.That(actualSettingsState, Is.EqualTo(expectedSettingsState));
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
