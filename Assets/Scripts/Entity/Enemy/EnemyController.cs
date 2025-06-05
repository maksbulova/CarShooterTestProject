using System;
using PoolSystem;
using UnityEngine;
using Zenject;

public class EnemyController : MonoBehaviour, IPoolableItem
{
    [Inject] private EnemyBehaviourController _behaviourController;
    [Inject] private EnemyMovementController _movementController;
    [Inject] private HealthController _healthController;
    [Inject] private EnemyAnimationController _animationController;
    [Inject] private EnemyAttackController _attackController;
    [Inject] private PoolSystem.PoolSystem _poolSystem;
    [Inject] private PlayerController _player;
    
    private EnemyStats _enemyStats;
    
    public void Init(EnemyStats stats)
    {
        _enemyStats = stats;
        _healthController.Init(stats.Health);
        _movementController.Init(stats.MoveSpeed);
        _animationController.Init();
        _behaviourController.Init();
        _attackController.Init(_enemyStats);
    }

    public void CreateByPool()
    {
    }

    public void GetByPool()
    {
        _healthController.OnHit += Hit;
        _healthController.OnDeath += Death;
        gameObject.SetActive(true);
    }

    public void ReleaseByPool()
    {
        _healthController.OnHit -= Hit;
        _healthController.OnDeath -= Death;
        gameObject.SetActive(false);
    }

    public void DestroyByPool()
    {
    }

    private void Hit(HitData hitData)
    {
        _animationController.PlayHit(hitData);
    }

    private void Death(HitData hitData)
    {
        _animationController.PlayDeath(hitData);
        Despawn();
    }

    private void Despawn()
    {
        _healthController.Deinit();
        _movementController.Deinit();
        _behaviourController.Death();
        _poolSystem.Despawn(this);
    }

    public void CheckDespawn()
    {
        if (_player.transform.position.z > transform.position.z &&
            !_animationController.IsVisible)
        {
            Despawn();
        }
    }
}
