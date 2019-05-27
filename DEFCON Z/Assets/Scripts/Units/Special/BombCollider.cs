using DefconZ.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollider : MonoBehaviour
{
    public float bombDamage;

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError($"Hit: {other.name}");

        IDestructible obj = other.GetComponent<IDestructible>();

        if (obj != null)
        {
            Debug.Log($"Damaged {other.name} for {bombDamage} HP");
            obj.TakeDamage(bombDamage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogError($"Hit: {collision.gameObject.name}");
    }
}
