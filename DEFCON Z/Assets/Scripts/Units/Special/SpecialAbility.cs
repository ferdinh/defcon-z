using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.Units.Special
{
    public enum AbilityType
    {
        PrecisionBomb
    }

    public class SpecialAbility : MonoBehaviour
    {

        public float resourceCost;
        public string abilityName;
        

        public virtual void StartAbility()
        {

        }

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