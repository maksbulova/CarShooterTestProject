using UnityEngine;

namespace Enemy.Behaviour
{
    public class AttackState : IBehaviourState
    {
        private Transform _myTransform;
        private Collider _targetHitbox;
    
        private EnemyMovementController _movementController;
        private EnemyAttackController _attackController;
        private AttackStateContext _stateContext;

        public AttackState(AttackStateContext context, Transform myTransform, Collider targetHitbox,  
            EnemyMovementController movementController, EnemyAttackController attackController)
        {
            _stateContext = context;
            _myTransform = myTransform;
            _targetHitbox = targetHitbox;
            _movementController = movementController;
            _attackController = attackController;
        }

        public void Enter()
        {
            _movementController.SetSpeedMultiplier(1);
        }

        public void Update()
        {
            var targetPosition = _targetHitbox.ClosestPoint(_myTransform.position);
            _movementController.SetDestination(targetPosition, false);
            if ((_myTransform.position - targetPosition).sqrMagnitude <=
                _stateContext.AttackCheckDistance * _stateContext.AttackCheckDistance)
            {
                _attackController.Attack();
            }
        }

        public void Exit()
        {
        }
    }
}
