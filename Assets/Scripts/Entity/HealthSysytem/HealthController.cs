using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HealthController : MonoBehaviour, IDamageable
{
    [SerializeField] private Collider hitBox;
    
    [Inject] private DamageableProvider _damageableProvider;
    
    public event Action OnDeath;
    public event Action OnHit;

    private float _currentHealth;

    public void Init(float initialHealth)
    {
        _currentHealth = initialHealth;
        _damageableProvider.RegisterDamageable(hitBox, this);
    }

    public void Deinit()
    {
        _damageableProvider.UnregisterDamageable(hitBox);
    }
    
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth > 0)
            OnHit?.Invoke();
        else
            OnDeath?.Invoke();
    }
}
