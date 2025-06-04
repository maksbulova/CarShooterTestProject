using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [Inject] private EnemyAnimationController _animationController;
    
    private float _maxMoveSpeed;
    
    public void Init(float moveSpeed)
    {
        _maxMoveSpeed = moveSpeed;
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        var relativeSpeed = agent.velocity.sqrMagnitude / (_maxMoveSpeed * _maxMoveSpeed);
        _animationController.SetMoveSpeed(relativeSpeed);
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        agent.speed = _maxMoveSpeed * multiplier;
    }

    public void SetDestination(Vector3 target)
    {
        agent.isStopped = false;
        agent.SetDestination(target);
    }
    
    public void Deinit()
    {
        agent.isStopped = true;
    }
}
