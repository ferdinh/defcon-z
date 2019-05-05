using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace DefconZ
{
    /// <summary>
    /// Class for the settings menu, containing methods for applying settings
    /// </summary>
    public class SettingsMenu : MonoBehaviour
    {
        /// <summary>
        /// Sets the difficulty by the provided level amount
        /// </summary>
        /// <param name="difficulty">Int from 1-3, higher the number, harder the difficulty</param>
        public void SetDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case 1:
                    Debug.Log("Difficulty set to Easy");
                    break;
                case 2:
                    Debug.Log("Difficulty set to Normal");
                    break;
                case 3:
                    Debug.Log("Difficulty set to Hard");
                    break;
                default:
                    break;
            }
        }
    }
}