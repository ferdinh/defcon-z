using DefconZ.Simulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefconZ.UI
{
    /// <summary>
    /// Class for the settings menu, containing methods for applying settings
    /// </summary>
    public class SettingsMenu : MonoBehaviour
    {
        public Dropdown difficultyDropdown;
        private GameObject previousMenu;

        public Slider musicVolume;
        public Slider gameVolume;
        public AudioSource audioSource;
        public bool initialised = false;

        void Start()
        {
            this.gameObject.SetActive(false);
            audioSource = GameObject.Find("Game Music").GetComponent<AudioSource>();
            AudioListener.volume = gameVolume.value;
            musicVolume.value = audioSource.volume;
            initialised = true;
        }

        /// <summary>
        /// Displays difficulty drop down box with various difficulty's
        /// </summary>
        public void OnDifficultyChange()
        {
            SetDifficulty(difficultyDropdown.value);
        }

        /// <summary>
        /// Sets the difficulty by the provided level amount
        /// </summary>
        /// <param name="difficulty">Int from 0-2, higher the number, harder the difficulty</param>
        public void SetDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    Debug.Log("Difficulty set to Easy");
                    Difficulty.SelectedDifficulty = Difficulty.Easy;
                    
                    break;
                case 1:
                    Debug.Log("Difficulty set to Normal");
                    Difficulty.SelectedDifficulty = Difficulty.Normal;
                    break;
                case 2:
                    Debug.Log("Difficulty set to Hard");
                    Difficulty.SelectedDifficulty = Difficulty.Hard;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Swaps the active state of the provided game object, and the game object this script is attached to
        /// </summary>
        /// <param name="other"></param>
        public void ToggleActive(GameObject other)
        {
            previousMenu = other;

            bool selfActive = gameObject.activeSelf;

            selfActive = (selfActive) ? false : true;
            bool otherActive = (selfActive) ? false : true;

            other.SetActive(otherActive);
            gameObject.SetActive(selfActive);
        }

        /// <summary>
        /// Returns back to the previous menu
        /// </summary>
        public void ReturnButton()
        {
            ToggleActive(previousMenu);
        }

        /// <summary>
        /// Adjusts the audio listener volume to value from master volume slider 
        /// </summary>
        public void OnGameSoundChange()
        {
            float value = Mathf.Clamp(gameVolume.value, 0.0f, 1.0f);
            AudioListener.volume = value;
            Debug.Log($"Master Volume set to: {value}");
        }

        /// <summary>
        /// Adjusts the audio source volume to value from music volume slider 
        /// </summary>
        public void OnMusicVolumeChange()
        {
            Settings.SetMusicVolume(musicVolume.value);
            audioSource.volume = Settings.MusicLevel.Value;
            Debug.Log($"Volume set to: {Settings.MusicLevel.Value}");
        }
    }
}