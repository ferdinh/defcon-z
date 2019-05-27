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
    }
}