using System;
using System.Collections;
using System.Collections.Generic;
using BitToolSet;
using UnityEngine;
using Zenject;

public class EnemyBehaviourController : MonoBehaviour
{
    [SerializeField] private InteractionTrigger interactionTrigger;

    [Inject] private DamageableProvider _damageableProvider;
    
    private EnemyBehaviourStateMachine _enemyBehaviourStateMachine;
    private IDamageable _currentAttackTarget;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _enemyBehaviourStateMachine = new EnemyBehaviourStateMachine();
        interactionTrigger.OnTriggerEnterE += OnInteractionStart;
        interactionTrigger.OnTriggerExitE += OnInteractionEnd;
    }

    private void OnInteractionStart(Collider collider)
    {
        if (!_damageableProvider.TryGetDamageable(collider, out IDamageable damageable))
            return;
        
        _currentAttackTarget = damageable;
        // _enemyBehaviourStateMachine.SetState(new AttackState());
    }

    private void OnInteractionEnd(Collider collider)
    {
        if (_damageableProvider.TryGetDamageable(collider, out IDamageable damageable) &&
            _currentAttackTarget == damageable)
        {
            // _enemyBehaviourStateMachine.SetState(new WanderState());
        }
    }
}
