using DefconZ.Simulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    Difficulty.SelectDifficulty = Difficulty.Easy;
                    //gameManager.SetDifficulty(Difficulty.Easy);
                    
                    break;
                case 2:
                    Debug.Log("Difficulty set to Normal");
                    Difficulty.SelectDifficulty = Difficulty.Normal;
                    break;
                case 3:
                    Debug.Log("Difficulty set to Hard");
                    //gameManager.SetDifficulty(Difficulty.Hard);
                    Difficulty.SelectDifficulty = Difficulty.Hard;
                    break;
                default:
                    break;
            }
        }
    }
}