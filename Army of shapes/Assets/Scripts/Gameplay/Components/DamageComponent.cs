using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathParticleSystem;
    [SerializeField] private ProgressBar _healthBar;
    [SerializeField] private GameEvent DieEvent;
    private UnitComponent _unitComponent;
    
   
    private float _health;
    private float _maxHealth;
    private void Start()
    {
        _unitComponent = gameObject.GetComponent<UnitComponent>();
        _health = _unitComponent.GetHealth();
        _maxHealth = _health;
    }

    public void TakeDamage(float attackDamage)
    {
        _health -= attackDamage;
        if (_health <= 0)
        {
            _healthBar.SetProgress(0, 1.5f);
            _deathParticleSystem.Play();
            StartCoroutine(KillUnit());
        }
        else
        {
            _healthBar.SetProgress(_health/_maxHealth, 1.5f);
        }
    }

    private IEnumerator KillUnit()
    {
        yield return null;
        yield return new WaitForSeconds(_deathParticleSystem.duration);
        _unitComponent.KillUnit();
        gameObject.SetActive(false);
        Destroy(_healthBar.gameObject);
        DieEvent.Raise();
    }

    public void SetUpHealthBar(Canvas canvas, Camera camera)
    {
        _healthBar.transform.SetParent(canvas.transform);
        if (_healthBar.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
            faceCamera.enabled = true;
        }
    }
}
