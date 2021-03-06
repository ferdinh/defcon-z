﻿using System;
using DefconZ.Simulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.Simulation
{
    /// <summary>
    /// Game difficulty preset.
    /// </summary>
    public static class Difficulty
    {
        public static Modifier Easy = new Modifier
        {
            Name = "Easy Difficulty",
            Type = ModifierType.Difficulty,
            Value = 0.5f
        };

        public static Modifier Normal = new Modifier
        {
            Name = "Normal Difficulty",
            Type = ModifierType.Difficulty,
            Value = 0.0f
        };

        public static Modifier Hard = new Modifier
        {
            Name = "Hard Difficulty",
            Type = ModifierType.Difficulty,
            Value = -0.5f
        };

        public static Modifier SelectedDifficulty; 
    }
}