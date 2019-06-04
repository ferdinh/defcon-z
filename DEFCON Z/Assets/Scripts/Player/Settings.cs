using DefconZ.Simulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ
{
    public static class Settings
    {
        public static Modifier MusicLevel = new Modifier
        {
            Name = "Music Volume Level",
            Type = ModifierType.SoundSetting,
            Value = 0.15f
        };

        public static void SetMusicVolume(float level)
        {
            level = Mathf.Clamp(level, 0.0f, 1.0f);
            MusicLevel.Value = level;
        }
    }
}