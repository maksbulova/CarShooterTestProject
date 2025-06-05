using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [Header("Movement stats")]
    [SerializeField] private float rotationSpeed = 90;

    [Inject] private EnemyAnimationController _animationController;
    
    private float _maxMoveSpeed;
    private CancellationTokenSource _rotationCancellationTokenSource;

    private const int TopAgentPriority = 0;
    private const int LeastAgentPriority = 99;
    
    public void Init(float moveSpeed)
    {
        _maxMoveSpeed = moveSpeed;
        agent.speed = moveSpeed;
        _rotationCancellationTokenSource = new CancellationTokenSource();
        agent.avoidancePriority = Random.Range(TopAgentPriority, LeastAgentPriority);
    }

    private void Update()
    {
        // Suboptimal, but left for precise animations. Better then velocity.magnitude
        var relativeSpeed = Mathf.Sqrt(agent.velocity.sqrMagnitude) / _maxMoveSpeed;
        _animationController.SetMoveSpeed(relativeSpeed);
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        agent.speed = _maxMoveSpeed * multiplier;
    }

    public async UniTaskVoid SetDestination(Vector3 target, bool rotateTowardTarget)
    {
        _rotationCancellationTokenSource.Cancel();

        if (rotateTowardTarget)
        {
            agent.isStopped = true;

            var direction = (target - transform.position).normalized;
            float rotationDuration = Vector3.Angle(transform.forward, direction) / rotationSpeed;
            for (float t = 0; t < rotationDuration; t += Time.fixedDeltaTime)
            {
                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction),
                        rotationSpeed * Time.fixedDeltaTime);

                await UniTask.NextFrame(_rotationCancellationTokenSource.Token);
            }
        }
        
        agent.isStopped = false;
        agent.SetDestination(target);
    }
    
    public void Deinit()
    {
        agent.isStopped = true;
        _rotationCancellationTokenSource.Cancel();
        _rotationCancellationTokenSource.Dispose();
    }

    private void OnDestroy()
    {
        _rotationCancellationTokenSource.Cancel();
        _rotationCancellationTokenSource.Dispose();
    }
}
