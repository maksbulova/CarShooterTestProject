using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyAttackController : MonoBehaviour
{
    [Inject] private EnemyAnimationController _animationController;
    [Inject] private DamageableProvider _damageableProvider;

    private EnemyStats _stats;

    private bool _readyToAttack;
    private float _lastAttackTime;

    private const int MaxAttackColliders = 5;
    
    public void Init(EnemyStats stats)
    {
        _stats = stats;
        
        _readyToAttack = true;
    }
    
    public void Attack()
    {
        if (!_readyToAttack && Time.time < _lastAttackTime + _stats.AttackCooldown)
            return;

        _lastAttackTime = Time.time;
        _readyToAttack = false;
        _animationController.PlayAttack(OnHitAnimationCallback);
    }

    private void OnHitAnimationCallback()
    {
        _lastAttackTime = Time.time;

        Collider[] colliders = new Collider[MaxAttackColliders];
        int hitAmount = Physics.OverlapSphereNonAlloc(transform.position, _stats.AttackRange, colliders, _stats.AttackLayer);
        for (int i = 0; i < hitAmount; i++)
        {
            if (_damageableProvider.TryGetDamageable(colliders[i], out IDamageable damageable))
            {
                var hitData = new HitData(_stats.Damage,
                    colliders[i].ClosestPoint(transform.position),
                    (colliders[i].transform.position - transform.position).normalized);
                damageable.TakeDamage(hitData);
                break;
            }
        }
    }
}
