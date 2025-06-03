using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public event Action OnDeath;

    private float _currentHealth;

    public void Init(float initialHealth)
    {
        _currentHealth = initialHealth;
    }
    
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            OnDeath?.Invoke();
    }
}
