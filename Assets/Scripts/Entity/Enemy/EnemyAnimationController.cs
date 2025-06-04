using System;
using System.Collections;
using System.Collections.Generic;
using Helper.PoolSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private FlickAnimator flickAnimator;
    [SerializeField] private PoolableParticle particleOnHit;
    [SerializeField] private PoolConfig hitParticlePoolConfig;
    [SerializeField] private PoolableParticle particleOnDeath;
    [SerializeField] private PoolConfig deathParticlePoolConfig;

    [Inject] private PoolSystem _poolSystem;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int TakeHit = Animator.StringToHash("TakeHit");


    public void Init()
    {
        animator.SetLayerWeight(0, 0);
        flickAnimator.Init();
        _poolSystem.InitPool<PoolableParticle>(hitParticlePoolConfig);
        _poolSystem.InitPool<PoolableParticle>(deathParticlePoolConfig);
    }

    public void SetMoveSpeed(float relativeSpeed)
    {
        animator.SetFloat(Speed, relativeSpeed);
    }

    public void PlayAttack(Action onHitCallback)
    {
        
    }

    public void StopAttack()
    {
        
    }
    
    public void PlayHit(HitData hitData)
    {
        flickAnimator.PlayFlick();
        animator.SetTrigger(TakeHit);
        PlayParticle(hitData, particleOnHit);
    }

    public void PlayDeath(HitData hitData)
    {
        PlayParticle(hitData, particleOnDeath);
    }

    private void PlayParticle(HitData hitData, PoolableParticle particle)
    {
        var splash = _poolSystem.Spawn(particle);
        splash.transform.position = hitData.HitPosition;
        splash.transform.rotation = Quaternion.LookRotation(hitData.HitDirection);
    }
}
