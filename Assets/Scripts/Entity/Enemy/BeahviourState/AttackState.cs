using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AttackState : IBehaviourState
{
    private Transform _myTransform;
    private Transform _targetTransform;
    
    private EnemyMovementController _movementController;
    private EnemyAttackController _attackController;
    
    // TODO extract to some scriptable config
    private float _attackCheckDistance = 0.5f;

    
    public AttackState(Transform myTransform, Transform targetTransform,  
        EnemyMovementController movementController, EnemyAttackController attackController)
    {
        _myTransform = myTransform;
        _targetTransform = targetTransform;
        _movementController = movementController;
        _attackController = attackController;
    }

    public void Enter()
    {
        _movementController.SetSpeedMultiplier(1);
    }

    public void Update()
    {
        _movementController.SetDestination(_targetTransform.position);

        if ((_myTransform.position - _targetTransform.position).sqrMagnitude <=
            _attackCheckDistance * _attackCheckDistance)
        {
            _attackController.Attack();
        }
    }

    public void Exit()
    {
    }
}
