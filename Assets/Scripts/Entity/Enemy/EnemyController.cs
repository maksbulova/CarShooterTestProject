using PoolSystem;
using UnityEngine;
using Zenject;

public class EnemyController : MonoBehaviour, IPoolableItem
{
    [SerializeField] private EnemyMovementController movementController;
    [SerializeField] private HealthController healthController;
    [SerializeField] private EnemyAnimationController animationController;

    [Inject] private PoolSystem.PoolSystem _poolSystem;
    
    private EnemyStats _enemyStats;
    
    public void Init(EnemyStats stats)
    {
        _enemyStats = stats;
        healthController.Init(stats.Health);
        movementController.Init(stats.MoveSpeed);
        animationController.Init();
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
        _poolSystem.Despawn(this);
        healthController.Deinit();
    }
}
