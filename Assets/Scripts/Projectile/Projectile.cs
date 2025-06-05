using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BitToolSet;
using Cysharp.Threading.Tasks;
using DamageSystem;
using PoolSystem;
using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour, IPoolableItem
{
    [SerializeField] private InteractionTrigger interactionTrigger;
    [SerializeField] private Rigidbody rigidbody;
    [Header("VFX")]
    [SerializeField] private PoolableParticle particleOnHit;
    [SerializeField] private PoolConfig particleOnHitConfig;

    [Inject] private DamageableProvider _damageableProvider;
    [Inject] private PoolSystem.PoolSystem _poolSystem;

    private ProjectileStats _stats;
    private CancellationTokenSource _despawnCancellationTokenSource = new CancellationTokenSource();
    
    public void Launch(ProjectileStats stats)
    {
        _stats = stats;
        rigidbody.velocity = stats.BulletVelocity * rigidbody.transform.forward;
        AutoDespawnDelay(stats.BulletLifeTime);
    }

    private async UniTaskVoid AutoDespawnDelay(float lifetime)
    {
        await UniTask.WaitForSeconds(lifetime, cancellationToken: _despawnCancellationTokenSource.Token);

        _poolSystem.Despawn(this);
    }

    private void OnProjectileHit(Collider collider)
    {
        if (!_damageableProvider.TryGetDamageable(collider, out IDamageable damageable)) 
            return;

        var hitEffect = _poolSystem.Spawn(particleOnHit);
        hitEffect.transform.position = transform.position;
        
        damageable.TakeDamage(new HitData(_stats.Damage, transform.position, rigidbody.velocity));
            
        _despawnCancellationTokenSource.Cancel();
        _poolSystem.Despawn(this);
    }

    public void CreateByPool()
    {
        _poolSystem.InitPool<PoolableParticle>(particleOnHitConfig);
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
