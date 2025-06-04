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
    [Inject] private EnemyMovementController _movementController;
    [Inject] private EnemyAttackController _attackController;
    [Inject] private Transform _rootTransform;
    
    private EnemyBehaviourStateMachine _enemyBehaviourStateMachine;
    private IDamageable _currentAttackTarget;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _enemyBehaviourStateMachine.Update();
    }

    public void Init()
    {
        _enemyBehaviourStateMachine = new EnemyBehaviourStateMachine();
        interactionTrigger.OnTriggerEnterE += OnInteractionStart;
        interactionTrigger.OnTriggerExitE += OnInteractionEnd;
        
        _enemyBehaviourStateMachine.SetState(new WanderState(_rootTransform, _movementController));
    }

    private void OnInteractionStart(Collider collider)
    {
        if (!_damageableProvider.TryGetDamageable(collider, out IDamageable damageable))
            return;
        
        _currentAttackTarget = damageable;
        _enemyBehaviourStateMachine.SetState(
            new AttackState(_rootTransform, collider.transform, 
                _movementController, _attackController));
    }

    private void OnInteractionEnd(Collider collider)
    {
        if (_damageableProvider.TryGetDamageable(collider, out IDamageable damageable) &&
            _currentAttackTarget == damageable)
        {
             _enemyBehaviourStateMachine.SetState(new WanderState(_rootTransform, _movementController));
        }
    }
}
