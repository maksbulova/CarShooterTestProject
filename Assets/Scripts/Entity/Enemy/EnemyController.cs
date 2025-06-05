using PoolSystem;
using UnityEngine;
using Zenject;

public class EnemyController : MonoBehaviour, IPoolableItem
{
    [Inject] private EnemyBehaviourController behaviourController;
    [Inject] private EnemyMovementController movementController;
    [Inject] private HealthController healthController;
    [Inject] private EnemyAnimationController animationController;
    [Inject] private EnemyAttackController attackController;
    [Inject] private PoolSystem.PoolSystem _poolSystem;
    
    private EnemyStats _enemyStats;
    
    public void Init(EnemyStats stats)
    {
        _enemyStats = stats;
        healthController.Init(stats.Health);
        movementController.Init(stats.MoveSpeed);
        animationController.Init();
        attackController.Init(_enemyStats);
    }

    public void CreateByPool()
    {
    }

    public void GetByPool()
    {
        healthController.OnHit += Hit;
        healthController.OnDeath += Death;
        gameObject.SetActive(true);
    }

    public void ReleaseByPool()
    {
        healthController.OnHit -= Hit;
        healthController.OnDeath -= Death;
        gameObject.SetActive(false);
    }

    public void DestroyByPool()
    {
    }

    private void Hit(HitData hitData)
    {
        animationController.PlayHit(hitData);
    }

    private void Death(HitData hitData)
    {
        animationController.PlayDeath(hitData);
        healthController.Deinit();
        movementController.Deinit();
        behaviourController.Death();
        _poolSystem.Despawn(this);
    }
}
