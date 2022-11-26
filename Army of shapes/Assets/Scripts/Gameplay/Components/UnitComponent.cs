using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitComponent : MonoBehaviour
{
    private Unit _unit;
    private int _enemyArmyLayerMask;

    private void Start()
    {
        Debug.Log("Oi fui uma unidade criada");
    }

    public void AttachUnit(Unit unit)
    {
        _unit = unit;
        SetupGameObejectWithUnit();
    }

    public void SetEnemyArmyLayerMask(int enemyArmyLayerMask)
    {
        _enemyArmyLayerMask = enemyArmyLayerMask;
    }

    private void SetupGameObejectWithUnit()
    {
        gameObject.GetComponent<Renderer>().material.color = _unit.Color;
        transform.localScale *= _unit.Size;
    }

    public float GetSpeed()
    {
        return _unit.Speed;
    }
    
    public TypeOfAttack GetTypeOfAttack()
    {
        return _unit.TypeOfAttack;
    }

    public float GetHealth()
    {
        return _unit.Health;
    }
    
    public float GetAttack()
    {
        return _unit.Attack;
    }
    
    public float GetAttackSpeed()
    {
        return _unit.AttackSpeed;
    }
    
    public Unit GetUnit()
    {
        return _unit;
    }
    
    public LayerMask GetEnemyArmyLayerMask()
    {
        return 1<<_enemyArmyLayerMask;
    }
}
