﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefconZ.Simulation
{
    /// <summary>
    /// A class that contains the available resources.
    /// </summary>
    public class Resource
    {
        public float BaseResourcePoint { get; }
        public float MaxResourcePoint { get; set; }
        public float MaxSciencePoint { get; set; }
        public float SciencePoint { get; set; }
        public float ResourcePoint { get; set; }

        public IList<Modifier> Modifiers { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        public Resource()
        {
            BaseResourcePoint = 10000.0f;
            Modifiers = new List<Modifier>();
        }

        /// <summary>
        /// Computes the starting resource value.
        /// </summary>
        public Resource ComputeStartingValue()
        {
            Debug.Log("Calculating starting resources.");

            // Look for the difficulty value.
            var diffMod = Modifiers.SingleOrDefault(mod => mod.Type.Equals(ModifierType.Difficulty));
            var diffModValue = 0.0f;

            // TODO: Science point will not be implemented in this sprint 1.
            SciencePoint = 100;

            // If the case where there is no modifier of type difficulty is found,
            // then the value will default to zero.
            if (diffMod != null)
            {
                diffModValue = diffMod.Value;
            }

            var resourceModValue = 0.3f + diffModValue + UnityEngine.Random.Range(0.05f, 0.1f);

            ResourcePoint = resourceModValue * MaxResourcePoint;

            Debug.Log("End starting resources.");

            return this;
        }

        /// <summary>
        /// Calculates the maximum points available.
        /// </summary>
        public Resource CalculateMaxPoints()
        {
            var modifierValue = 1.0f + Modifiers.Sum(mod => mod.Value);

            MaxResourcePoint = BaseResourcePoint * modifierValue;

            return this;
        }

        /// <summary>
        /// Computes the gain/loss in resources.
        /// </summary>
        /// <returns>Resource gathered</returns>
        public float GatherResource()
        {
            // Base resource replenish resource point from zero to full in
            // 3 years or 1095 days.
            float baseresourcePointIncrease = MaxResourcePoint / 1095.0f;
            float increaseModifier = 1.0f + Modifiers.Sum(mod => mod.Value);
            float resourcePointIncrease = baseresourcePointIncrease * increaseModifier;

            ResourcePoint += resourcePointIncrease;

            // Limit the available resource point to MaxResourcePoint.
            if (ResourcePoint > MaxResourcePoint)
            {
                ResourcePoint = MaxResourcePoint;
            }

            return resourcePointIncrease;
        }

        /// <summary>
        /// Uses the resource.
        /// </summary>
        /// <param name="resourceToUse">The amount of resource to use.</param>
        public void UseResource(float resourceToUse)
        {
            ResourcePoint -= resourceToUse;
        }
    }
}