using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    private UnitComponent _unitComponent;

    private float health;
    private void Start()
    {
        _unitComponent = gameObject.GetComponent<UnitComponent>();
        health = _unitComponent.GetHealth();
    }

    public void TakeDamage(float attackDamage)
    {
        health -= attackDamage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
