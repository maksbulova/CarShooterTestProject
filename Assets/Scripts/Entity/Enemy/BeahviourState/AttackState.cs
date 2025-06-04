using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AttackState : IBehaviourState
{
    private Transform _myTransform;
    private IDamageable _attackTarget;
    
    private EnemyMovementController _movementController;
    private EnemyAttackController _attackController;

    private CancellationTokenSource _cancellationTokenSource;
    
    private const float PathUpdateInterval = 0.25f;
    
    public AttackState(Transform myTransform, IDamageable attackTarget, 
        EnemyMovementController movementController, EnemyAttackController attackController)
    {
        _myTransform = myTransform;
        _attackTarget = attackTarget;
        _movementController = movementController;
        _attackController = attackController;

        _cancellationTokenSource = new CancellationTokenSource();
    }

    public void Enter()
    {
        MovementTick();
    }

    private async UniTaskVoid MovementTick()
    {
        while (true)
        {
            await UniTask.WaitForSeconds(PathUpdateInterval, cancellationToken: _cancellationTokenSource.Token);
        }
    }

    public void Exit()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }
}
