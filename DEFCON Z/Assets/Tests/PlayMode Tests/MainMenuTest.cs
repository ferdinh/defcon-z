﻿using DefconZ;
using DefconZ.UI;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    /// <summary>
    /// Test Suite for Main Menu.
    /// </summary>
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
        /// Main Menu should set Settings Panel to True.
        /// </summary>
        [Test]
        public void MainMenu_Should_Initialize_SettingsPanel_To_False()
        {
            // Arrange
            bool expectedSettingsPanelState = false;

            // Act
            // This part has been initiated in the Setup process.

            // Assert
            Assert.That(MainMenu.settingsPanel.activeSelf, Is.EqualTo(expectedSettingsPanelState));
        }

        /// <summary>
        /// Main Menu should set Menu Panel to True on first init.
        /// </summary>
        [Test]
        public void MainMenu_Should_Initialize_MenuPanel_To_True()
        {
            // Arrange
            bool expectedMenuPanelState = true;

            // Act
            // This part has been initiated in the Setup process.
            Assert.That(MainMenu.menuPanel.activeSelf, Is.EqualTo(expectedMenuPanelState));
        }

        /// <summary>
        /// Test ensures that Settings state and Main Menu Panel state is different.
        ///
        /// Toggling is not tested here as it is already tested in another test.
        /// </summary>
        [Test]
        public void MainMenu_Should_Have_DifferentState_Of_MenuPanels_And_Settings()
        {
            // Arrange
            bool expectedMainMenuPanelState = true;
            bool expectedSettingsState = false;

            // Act
            // This part has been initiated in the Setup process.

            // Assert
            Assert.That(MainMenu.menuPanel.activeSelf, Is.EqualTo(expectedMainMenuPanelState));
            Assert.That(MainMenu.settings, Is.EqualTo(expectedSettingsState));
        }

        /// <summary>
        /// Test ensures that main menu toggle as it should.
        ///
        /// In general, it expects main menu to show menu panel and hide settings
        /// panel when first initialized and flipping which panel to show when
        /// it is toggled, if menu panel = active, settings menu panel = inactive,
        /// toggle setting will swap the value to menu panel = inactive and
        /// settings menu panel = active.
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
    }
}