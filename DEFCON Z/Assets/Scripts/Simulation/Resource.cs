using DefconZ.GameLevel;
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

        /// <summary>
        /// Gets the maximum resource point.
        /// </summary>
        /// <value>
        /// The maximum resource point.
        /// </value>
        public float GetMaxResourcePoint
        {
            get
            {
                return CalculateMaxPoints();
            }
        }

        public float MaxSciencePoint { get; set; }
        public float SciencePoint { get; set; }
        public float ResourcePoint { get; set; }

        private ICollection<Modifier> Modifiers { get; }

        public ICollection<Zone> OwnedZones { get; }

        public Resource(ICollection<Modifier> modifiers)
        {
            BaseResourcePoint = 10000.0f;
            Modifiers = modifiers;
            OwnedZones = new List<Zone>();

            ComputeStartingValue();
        }

        /// <summary>
        /// Computes the starting resource value.
        /// </summary>
        private Resource ComputeStartingValue()
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

            ResourcePoint = resourceModValue * GetMaxResourcePoint;

            Debug.Log("End starting resources.");

            return this;
        }

        /// <summary>
        /// Calculates the maximum available resource points.
        /// </summary>
        private float CalculateMaxPoints()
        {
            var modifierValue = 1.0f + Modifiers.Where(mod => mod.Type == ModifierType.Difficulty).Sum(mod => mod.Value);
            var totalOwnedZones = OwnedZones.Sum(v => v.zoneResourceValue);

            return (BaseResourcePoint + totalOwnedZones) * modifierValue;
        }

        /// <summary>
        /// Computes the gain/loss in resources.
        /// </summary>
        /// <returns>Resource gathered.</returns>
        public float GatherResource()
        {
            // Base resource replenish resource point from zero to full in
            // 3 years or 1095 days.
            float baseresourcePointIncrease = GetMaxResourcePoint / 1095.0f;
            float increaseModifier = 1.0f + Modifiers.Sum(mod => mod.Value);
            float resourcePointIncrease = baseresourcePointIncrease * increaseModifier;

            Debug.LogError($"base: {baseresourcePointIncrease} Modifier: {increaseModifier} Total increase: {resourcePointIncrease}");

            ResourcePoint += resourcePointIncrease;

            // Limit the available resource point to MaxResourcePoint.
            if (ResourcePoint > GetMaxResourcePoint)
            {
                ResourcePoint = GetMaxResourcePoint;
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