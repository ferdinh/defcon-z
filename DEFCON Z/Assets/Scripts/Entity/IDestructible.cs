using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ
{
    public interface IDestructible
    {
        void DestroySelf();
        void TakeDamage(float damage);
    }
}

