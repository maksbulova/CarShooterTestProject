using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PoolSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationEventListener animationEventListener;
    [SerializeField] private float layerWeightChangeDuration = 0.5f;
    
    [SerializeField] private FlickAnimator flickAnimator;
    [SerializeField] private PoolableParticle particleOnHit;
    [SerializeField] private PoolConfig hitParticlePoolConfig;
    [SerializeField] private PoolableParticle particleOnDeath;
    [SerializeField] private PoolConfig deathParticlePoolConfig;

    [Inject] private PoolSystem.PoolSystem _poolSystem;

    private Action _hitCallback;
    private int _upperBodyLayerIndex;
    private Dictionary<int, Tween> _changeLayerWeightDictionary = new Dictionary<int, Tween>();
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int TakeHit = Animator.StringToHash("TakeHit");
    private static readonly int Attack1 = Animator.StringToHash("Attack1");
    private static readonly int Attack2 = Animator.StringToHash("Attack2");

    private static readonly int[] Attacks = new int[] { Attack1, Attack2 };

    private void Awake()
    {
        animationEventListener.OnHit += OnAnimationHit;
        _upperBodyLayerIndex = animator.GetLayerIndex("UpperBody");
    }

    private void OnDestroy()
    {
        animationEventListener.OnHit -= OnAnimationHit;
    }

    public void Init()
    {
        animator.SetLayerWeight(_upperBodyLayerIndex, 0);
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
        _hitCallback = onHitCallback;
        // animator.SetLayerWeight(_upperBodyLayerIndex, 1);
        SetLayerWeight(_upperBodyLayerIndex, 1);

        animator.SetTrigger(Attacks[Random.Range(0, Attacks.Length)]);
    }

    private void OnAnimationHit()
    {
        _hitCallback?.Invoke();
        _hitCallback = null;
        
        // animator.SetLayerWeight(_upperBodyLayerIndex, 0);
        SetLayerWeight(_upperBodyLayerIndex, 0);
    }

    private void SetLayerWeight(int layerIndex, float targetWeight)
    {
        if (_changeLayerWeightDictionary.TryGetValue(layerIndex, out Tween tween))
        {
            tween.Kill();
            _changeLayerWeightDictionary.Remove(layerIndex);
        }

        var newTween = DOVirtual.Float(animator.GetLayerWeight(layerIndex), targetWeight, layerWeightChangeDuration,
                weight => animator.SetLayerWeight(layerIndex, weight))
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => _changeLayerWeightDictionary.Remove(layerIndex));
        _changeLayerWeightDictionary.Add(layerIndex, newTween);
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
        // TODO haptic
    }

    private void PlayParticle(HitData hitData, PoolableParticle particle)
    {
        var splash = _poolSystem.Spawn(particle);
        splash.transform.position = hitData.HitPosition;
        splash.transform.rotation = Quaternion.LookRotation(hitData.HitDirection);
    }
}
