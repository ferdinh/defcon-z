using DefconZ.Entity.Action;
using DefconZ.UI;
using DefconZ.Units;
using DefconZ.Units.Actions;
using DefconZ.Units.Special;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ
{
    public class SpecialAbilities : MonoBehaviour
    {
        public GameObject precisionBombPrefab;

        public void PrecisionBombAbility(Vector3 target, Vector3 eulerAngle, GameObject cam, Faction faction)
        {

            if(PrecisionBomb.CanAfford(faction, PrecisionBomb.abilityCost))
            {
                faction.Resource.ResourcePoint -= PrecisionBomb.abilityCost;
                GameObject bomb = Instantiate(precisionBombPrefab, new Vector3(0, -100, 0), Quaternion.identity);
                PrecisionBomb precisionBomb = bomb.GetComponent<PrecisionBomb>();
                precisionBomb.StartAbility(target, eulerAngle, cam);
            }
            
        }
    }
}
