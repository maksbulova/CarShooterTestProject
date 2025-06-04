
using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class WanderState : IBehaviourState
{
    private EnemyMovementController _movementController;
    private Transform _myTransform;
    
    private Vector3 _currentTarget;

    // TODO extract to some scriptable config
    private float _wanderRange = 3;
    private Vector2 _wanderSpeedMultiplier = new Vector2(0.25f, 0.75f);
    private float _destinationCheckDistance = 0.5f;
    
    public WanderState(Transform myTransform, EnemyMovementController movementController)
    {
        _myTransform = myTransform;
        _movementController = movementController;
    }

    public void Enter()
    {
        ChooseNewWanderPosition();
    }

    public void Update()
    {
        if ((_myTransform.position - _currentTarget).sqrMagnitude <= _destinationCheckDistance * _destinationCheckDistance)
        {
            ChooseNewWanderPosition();
        }
    }

    private void ChooseNewWanderPosition()
    {
        Vector3 randomOffset = Random.insideUnitSphere * _wanderSpeedMultiplier;
        randomOffset.y = 0;
        _currentTarget = _myTransform.position + randomOffset;
        _movementController.SetDestination(_currentTarget);
        _movementController.SetSpeedMultiplier(Random.Range(_wanderSpeedMultiplier.x, _wanderSpeedMultiplier.y));
    }

    public void Exit()
    {
    }
}
