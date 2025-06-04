using Helper.PoolSystem;
using UnityEngine;
using Zenject;

public class EnemyController : MonoBehaviour, IPoolableItem
{
    [SerializeField] private HealthController healthController;
    [SerializeField] private EnemyAnimationController animationController;

    [Inject] private PoolSystem _poolSystem;
    
    private EnemyStats _enemyStats;
    
    public void Init(EnemyStats stats)
    {
        _enemyStats = stats;
        healthController.Init(stats.Health);
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

    private void Hit()
    {
        // TODO animation
    }

    private void Death()
    {
        // TODO animation
        _poolSystem.Despawn(this);
        healthController.Deinit();
    }

    public void TakeDamage(float damage)
    {
        healthController.TakeDamage(damage);
    }
}
