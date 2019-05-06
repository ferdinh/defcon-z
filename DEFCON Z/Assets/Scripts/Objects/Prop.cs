using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ
{
    public class Prop : ObjectBase, IDestructible
    {
        public float health;

        public void DestroySelf()
        {
            Debug.Log(this.objName + " has reached 0 or less health and has been destroyed");
            Destroy(gameObject); // Remove the game object this script is attached to
        }

        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0.0f)
            {
                DestroySelf();
            }
        }
    }
}