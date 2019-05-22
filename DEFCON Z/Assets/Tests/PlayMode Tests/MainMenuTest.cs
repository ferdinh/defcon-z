using DefconZ.UI;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    /// <summary>
    /// Test Suite for Main Menu.
    /// </summary>
    public class MainMenuTest
    {
        private GameObject MainMenuGameObject = new GameObject();
        private GameObject SettingsMenuGameObject = new GameObject();
        private GameObject MusicSliderObject = new GameObject();
        private GameObject VolumeSliderObject = new GameObject();
        private GameObject AudioSourceGameObject;
        private MainMenu MainMenu;
        private SettingsMenu SettingsMenu;

        /// <summary>
        /// Setup test data for each test.
        /// </summary>
        [SetUp]
        public void TestInit()
        {
            MainMenuGameObject = new GameObject();
            SettingsMenuGameObject = new GameObject();
            AudioSourceGameObject = new GameObject();
            MusicSliderObject = new GameObject();
            VolumeSliderObject = new GameObject();
            SettingsMenuGameObject.name = "SettingsMenuCanvas";
            AudioSourceGameObject.name = "Game Music";

            // Object is disabled to prevent init code being called.
            MainMenuGameObject.SetActive(false);
            SettingsMenuGameObject.SetActive(false);

            // Set up the components.

            MainMenuGameObject.AddComponent<AudioListener>();
            MainMenu = MainMenuGameObject.AddComponent<MainMenu>();

            SettingsMenu = SettingsMenuGameObject.AddComponent<SettingsMenu>();
            MainMenu.settingsMenu = SettingsMenu;
            SettingsMenu.audioSource = AudioSourceGameObject.AddComponent<AudioSource>();

            SettingsMenu.gameVolume = VolumeSliderObject.AddComponent<Slider>();
            SettingsMenu.musicVolume = MusicSliderObject.AddComponent<Slider>();
            // sets slider music volume value
            SettingsMenu.musicVolume.value = 0.1f;

            //MainMenu.settingsPanel = new GameObject();
            MainMenu.menuPanel = new GameObject();

            // Reactivate the object to init the components.
            MainMenuGameObject.SetActive(true);
            SettingsMenuGameObject.SetActive(true);
            SettingsMenuGameObject.SetActive(false);
            //MainMenu.settingsMenu = SettingsMenu;
        }

        IEnumerator WaitFor(int sec)
        {
            Debug.LogError("Start wait");
            yield return new WaitForSeconds(sec);
            Debug.LogError("End Wait");
        }

        /// <summary>
        /// Clean up data for each test.
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            MainMenuGameObject = null;
            MainMenu = null;
            SettingsMenu = null;
            MainMenuGameObject = null;
            SettingsMenuGameObject = null;
            AudioSourceGameObject = null;
            MusicSliderObject = null;
            VolumeSliderObject = null;

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
            //GameObject settingsPanel = MainMenu.settingsMenuObject;
            //GameObject settingsPanel = MainMenu.settingsMenuObject;

            // Assert
            Assert.That(SettingsMenuGameObject.activeSelf, Is.EqualTo(expectedSettingsPanelState));
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
            Assert.That(SettingsMenuGameObject.activeSelf, Is.EqualTo(expectedSettingsState));
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
        [TestCase(false, true, false, true)]
        [TestCase(true, false, true, false)]
        public void MainMenu_Should_ToggleSettings(bool initialSettingsState, bool initialMenuState, bool expectedMenuPanelState, bool expectedSettingsPanelState)
        {
            // Act
            SettingsMenuGameObject.SetActive(initialSettingsState);
            MainMenuGameObject.SetActive(initialMenuState);
            MainMenu.ToggleSettings();
          
            // Assert
            Debug.Log($"Initial setting state: {initialSettingsState}");

            // Assert Menu Panel state.
            bool actualMenuPanelState = MainMenuGameObject.activeSelf;
            Debug.Log($"Menu panel state: {actualMenuPanelState}, expected: {expectedMenuPanelState}");
            Assert.That(actualMenuPanelState, Is.EqualTo(expectedMenuPanelState));

            // Assert Settings Panel state.
            bool actualSettingsPanelState = SettingsMenu.gameObject.activeSelf;
            Debug.Log($"Settings panel state: {actualSettingsPanelState}, expected: {expectedSettingsPanelState}");
            Assert.That(actualSettingsPanelState, Is.EqualTo(expectedSettingsPanelState));
        }
    }
}