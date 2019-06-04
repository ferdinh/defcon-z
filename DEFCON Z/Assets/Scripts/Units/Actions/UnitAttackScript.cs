using DefconZ;
using DefconZ.Entity.Action;
using DefconZ.Units;
using DefconZ.Units.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackScript : MonoBehaviour
{
    [SerializeField] private UnitBase unit;
    [SerializeField] private UnitBase enemyUnit;
    [SerializeField] private UnitMoveScript unitMoveScript;
    [SerializeField] private bool inCombat;
    [SerializeField] private float nextAttackTime;
    [SerializeField] public Faction enemyFaction;

    private void Awake()
    {
        unit = gameObject.GetComponent<UnitBase>();
        unitMoveScript = gameObject.GetComponent<UnitMoveScript>();
        enemyUnit = null;
        nextAttackTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inCombat)
        {
            if (enemyFaction != null)
            {
                CheckSightRadius();
            }
        }
        else
        {
            if (enemyUnit != null)
            {
                Attack();
            }
            else
            {
                RemoveCombat();
            }
        }
    }

    /// <summary>
    /// Orders the unit to attack the current enemy
    /// </summary>
    private void Attack()
    {
        // Check that the unit is close enough to attack
        if (Vector3.Distance(unit.transform.position, enemyUnit.transform.position) <= unit.attackRange)
        {
            // If the unit is moving, it needs to stop
            if (unitMoveScript.IsMoving)
            {
                unitMoveScript.StopMoving();
            }

            // Check that the cooldown has passed
            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + unit.attackTime;
                enemyUnit.TakeDamageFrom(unit);
                unit.PlayAttackSound();

                // If the enemy unit died, stop attacking
                if (!enemyUnit.IsAlive())
                {
                    RemoveCombat();
                }
            }
        }
        else
        {
            // Unit needs to move closer
            unit.GetComponent<IMoveable>().MoveTo(enemyUnit.gameObject);
        }
    }

    /// <summary>
    /// Checks for enemy units in the sight radius of the unit
    /// </summary>
    private void CheckSightRadius()
    {
        GameObject enemyObj = null;
        float distance = 999999;
        // Check if any enemy units are in range
        foreach (GameObject other in enemyFaction.Units)
        {
            float distanceToUnit = Vector3.Distance(unit.transform.position, other.transform.position);

            if (distanceToUnit <= unit.sightRange && distanceToUnit < distance)
            {
                if (other.GetComponent<UnitBase>().IsAlive())
                {
                    distance = distanceToUnit;
                    enemyObj = other;
                }
            }
        }

        if (enemyObj != null)
        {
            // Attack this unit
            StartAttack(enemyObj);
            Debug.Log($"{unit.objName} is attacking {enemyUnit}");
        }
    }

    /// <summary>
    /// Starts an attack
    /// </summary>
    /// <param name="enemy"></param>
    public void StartAttack(GameObject enemy)
    {
        inCombat = true;
        enemyUnit = enemy.GetComponent<UnitBase>();
    }

    /// <summary>
    /// Removes an attack
    /// </summary>
    public void RemoveCombat()
    {
        inCombat = false;
        enemyUnit = null;
    }

    /// <summary>
    /// Returns whether the unit is engaged in combat or not
    /// </summary>
    /// <returns></returns>
    public bool InCombat()
    {
        return inCombat;
    }
}
