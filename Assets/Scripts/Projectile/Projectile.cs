using System.Collections;
using System.Collections.Generic;
using System.Threading;
using BitToolSet;
using Cysharp.Threading.Tasks;
using Helper.PoolSystem;
using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour, IPoolableItem
{
    [SerializeField] private InteractionTrigger interactionTrigger;
    [SerializeField] private Rigidbody rigidbody;

    [Inject] private DamageableProvider _damageableProvider;
    [Inject] private PoolSystem _poolSystem;

    private ProjectileStats _stats;
    private CancellationTokenSource _despawnCancellationTokenSource = new CancellationTokenSource();
    
    public async UniTaskVoid Launch(ProjectileStats stats)
    {
        _stats = stats;
        rigidbody.velocity = stats.BulletVelocity * rigidbody.transform.forward;
        
        await UniTask.WaitForSeconds(stats.BulletLifeTime, cancellationToken: _despawnCancellationTokenSource.Token);
        
        _poolSystem.Despawn(this);
    }
    
    private void OnProjectileHit(Collider collider)
    {
        if (_damageableProvider.TryGetDamageable(collider, out IDamageable damageable))
        {
            damageable.TakeDamage(_stats.Damage);
            
            _despawnCancellationTokenSource.Cancel();
            _poolSystem.Despawn(this);
        }
    }

    public void CreateByPool()
    {
    }

    public void GetByPool()
    {
        ResetVelocity();
        interactionTrigger.SetInteractable(true);
        interactionTrigger.OnTriggerEnterE += OnProjectileHit;
        gameObject.SetActive(true);
    }

    public void ReleaseByPool()
    {
        _despawnCancellationTokenSource.Cancel();
        ResetVelocity();
        interactionTrigger.SetInteractable(false);
        interactionTrigger.OnTriggerEnterE -= OnProjectileHit;
        gameObject.SetActive(false);
    }

    public void DestroyByPool()
    {
        _despawnCancellationTokenSource.Dispose();
    }

    private void ResetVelocity()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
