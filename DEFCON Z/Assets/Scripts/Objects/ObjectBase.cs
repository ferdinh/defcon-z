using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ
{
    /// <summary>
    /// GameObject.cs defines variables and methods for the behaviour of custom game objects
    /// </summary>
    public abstract class ObjectBase : MonoBehaviour
    {
        public bool destructable;
        public float health;
        public string ObjName;

        /// <summary>
        /// Damages the object
        /// </summary>
        /// <param name="damage"> float representing the amount of damage being delt to the object</param>
        public abstract void DamageObject(float damage);

        /// <summary>
        /// Destroys the object
        /// </summary>
        public abstract void DestroySelf();

    }
}