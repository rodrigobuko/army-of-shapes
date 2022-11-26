using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    
    private UnitComponent _unitComponent;
    private float _attackSpeed;
    private float _attackDamage;

    private bool _attacked;
    private bool _attackCooldown;
    private void Start()
    {
        _unitComponent = gameObject.GetComponent<UnitComponent>();
        _attackDamage = _unitComponent.GetAttack();
        _attackSpeed = _unitComponent.GetAttackSpeed();
    }

    public void Attack(DamageComponent enemyDamageComponent)
    {
        if (!_attacked)
        {
            _attacked = true;
            enemyDamageComponent.TakeDamage(_attackDamage);
        }
        else if(!_attackCooldown)
        {
            _attackCooldown = true;
            StartCoroutine(WaitCooldown());
        }
    }

    private IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(_attackSpeed);
        _attacked = false;
        _attackCooldown = false;
        
    }
    
}
