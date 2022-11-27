using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitComponent : MonoBehaviour
{
    private Unit _unit;
    private bool _isDead;
    private int _enemyArmyLayerMask;
    [SerializeField] private TextMesh _armyTag;

    private void Start()
    {
        _isDead = false;
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
    
    public void SetEnemyArmyName(int armyNumber, Camera camera)
    {
        _armyTag.text = $"{armyNumber}";
        _armyTag.color = armyNumber == 1 ? Color.red : Color.cyan;
        if (_armyTag.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
            faceCamera.enabled = true;
        }
    }

    private void SetupGameObejectWithUnit()
    {
        gameObject.GetComponent<Renderer>().material.color = _unit.Color;
        transform.localScale *= _unit.Size;
    }

    public float GetSpeed()
    {
        if (_unit.Speed <=0)
        {
            return 1f;
        }
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

    public void KillUnit()
    {
        _isDead = true;
    }

    public bool IsDead => _isDead;
}
