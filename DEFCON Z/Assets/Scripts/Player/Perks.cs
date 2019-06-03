using DefconZ.Simulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ
{
    public static class Perks
    {
        public static Modifier SmallResourceBoost = new Modifier
        {
            Name = "Small Resource Boost",
            Type = ModifierType.ResourceBoost,
            Value = 0.25f
        };

        public static Modifier MediumResourceBoost = new Modifier
        {
            Name = "Medium Resource Boost",
            Type = ModifierType.ResourceBoost,
            Value = 0.5f
        };

        public static Modifier LargeResourceBoost = new Modifier
        {
            Name = "Large Resource Boost",
            Type = ModifierType.ResourceBoost,
            Value = 1.0f
        };
    }
}