using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.Units.Special
{
    /// <summary>
    /// Enum stores available ability types
    /// </summary>
    public enum AbilityType
    {
        PrecisionBomb
    }

    /// <summary>
    /// Base Class for a special ability
    /// </summary>
    public class SpecialAbility : MonoBehaviour
    {
        public float resourceCost;
        public string abilityName;
        
        /// <summary>
        /// Starts the special ability
        /// </summary>
        public virtual void StartAbility() {}

        public static bool CanAfford(Faction faction, float cost)
        {
            Debug.LogError($"Ability will cost ${cost}");
            if (faction.Resource.ResourcePoint >= cost)
            {
                return true;
            }

            return false;
        }
    }
}