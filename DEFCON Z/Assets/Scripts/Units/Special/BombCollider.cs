using DefconZ.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.Units.Special
{
    public class BombCollider : MonoBehaviour
    {
        public float bombDamage;

        private void OnTriggerEnter(Collider other)
        {
            IDestructible destructible = other.GetComponent<IDestructible>();

            if (destructible != null)
            {
                Debug.Log($"Damaged {other.name} for {bombDamage} HP");
                destructible.TakeDamage(bombDamage);
            }
        }
    }
}