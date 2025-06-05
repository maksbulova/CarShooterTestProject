using System;
using Cinemachine;
using DamageSystem;
using UnityEngine;
using Zenject;

public class HealthController : MonoBehaviour, IDamageable
{
    [SerializeField] private Collider hitBox;
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private TextPopUp textPopUp;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    
    [Inject] private DamageableProvider _damageableProvider;
    
    public event Action<HitData> OnDeath;
    public event Action<HitData> OnHit;

    private float _currentHealth;
    private float _initialHealth;

    public void Init(float initialHealth)
    {
        _initialHealth = initialHealth;
        _currentHealth = initialHealth;
        _damageableProvider.RegisterDamageable(hitBox, this);
        
        healthDisplay.HideInstant();
        healthDisplay.SetHealth(_currentHealth, 0, _initialHealth);
    }

    public void Deinit()
    {
        _damageableProvider.UnregisterDamageable(hitBox);
    }

    public void TakeDamage(HitData hitData)
    {
        if (_currentHealth < 0)
            return;
        
        _currentHealth -= hitData.Damage;
        
        Color popUpColor;
        if (_currentHealth > 0)
        {
            OnHit?.Invoke(hitData);
            popUpColor = Color.white;
            healthDisplay.Show();
        }
        else
        {
            OnDeath?.Invoke(hitData);
            popUpColor = Color.red;
            healthDisplay.Hide();
        }

        healthDisplay.TakeDamage(hitData.Damage, _currentHealth, 0, _initialHealth);
        var damageText = Mathf.RoundToInt(hitData.Damage).ToString();
        textPopUp.PlayText(damageText, hitData.HitPosition, popUpColor);

        var force = 1 - _currentHealth / _initialHealth;
        impulseSource?.GenerateImpulseWithForce(force);
    }
}
