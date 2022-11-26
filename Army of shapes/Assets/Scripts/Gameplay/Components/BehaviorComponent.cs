﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitStates
{
    Idle,
    Attacking,
    Moving
}
public class BehaviorComponent : MonoBehaviour
{
    [SerializeField] private float rangeMovePerception;
    [SerializeField] private float rangeAttackPerception;
    
    private AttackComponent _attackComponent;
    private MoveComponent _moveComponent;
    private UnitComponent _unitComponent;
    private UnitStates _unitState;

    // Start is called before the first frame update
    void Start()
    {
        _unitState = UnitStates.Idle;
        _attackComponent = gameObject.GetComponent<AttackComponent>();
        _moveComponent = gameObject.GetComponent<MoveComponent>();
        _unitComponent = gameObject.GetComponent<UnitComponent>();
    }
    
    void Update()
    {
        ComputeBehavior();
    }

    void ComputeBehavior()
    {
        // check attack
        Tuple<bool, Collider> enemyAttackFoundTuple = CheckEnemyAttackPerceived();
        if (enemyAttackFoundTuple.Item1)
        {
            _attackComponent.Attack(enemyAttackFoundTuple.Item2.GetComponent<DamageComponent>());
            _unitState = UnitStates.Attacking;
        }
        else
        {
            // check movement 
            Tuple<bool, Collider> enemyMoveFoundTuple = CheckEnemyMovePerceived();
            if (enemyMoveFoundTuple.Item1)
            {
                _moveComponent.MoveTo(enemyMoveFoundTuple.Item2.transform.position);
                _unitState = UnitStates.Moving;
            }
            else
            {
                // unit is on Idle
                _unitState = UnitStates.Idle;
            }
        }
        
        // switch (_unitState)
        // {
        //     case UnitStates.Idle:
        //         Debug.Log("Idle");
        //         break;
        //     case UnitStates.Moving:
        //         Debug.Log("Moving");
        //         break;
        //     case UnitStates.Attacking:
        //         Debug.Log("Attacking");
        //         break;
        // }
    }

    Tuple<bool, Collider> CheckEnemyMovePerceived()
    {
        bool findEnemy = false;
        Collider enemyCollider = null;
        //RaycastHit[] hits = Physics.SphereCastAll(transform.position, 1f, Vector3.up, 200f, _unitComponent.GetEnemyArmyLayerMask());
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100f, layerMask: _unitComponent.GetEnemyArmyLayerMask());
        float minDistance = float.MaxValue;
        foreach (var collider in colliders)
        {
            float distance = Vector3.Distance(collider.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                enemyCollider = collider;
                findEnemy = true;
            }
        }

        return new Tuple<bool, Collider>(findEnemy, enemyCollider);;
    }

    Tuple<bool,Collider> CheckEnemyAttackPerceived()
    {
        bool findEnemy = false;
        Collider collider = null;
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 2f, Vector3.up, 3f, _unitComponent.GetEnemyArmyLayerMask());
        switch (_unitComponent.GetTypeOfAttack())
        {
            case TypeOfAttack.AttackClosest:
                float minDistance = float.MaxValue;
                foreach (var hit in hits)
                {
                    if (hit.distance < minDistance)
                    {
                        minDistance = hit.distance;
                        collider = hit.collider;
                        findEnemy = true;
                    }
                }
                break;
            case TypeOfAttack.AttackLessHealth:
                float minHealth = float.MaxValue;
                foreach (var hit in hits)
                {
                    Unit hitUnit = hit.collider.GetComponent<UnitComponent>().GetUnit();
                    if (minHealth < hitUnit.Health)
                    {
                        minHealth = hitUnit.Health;
                        collider = hit.collider;
                        findEnemy = true;
                    }
                }
                break;
        }
        
        return new Tuple<bool, Collider>(findEnemy, collider);
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 200f);
    }
}
