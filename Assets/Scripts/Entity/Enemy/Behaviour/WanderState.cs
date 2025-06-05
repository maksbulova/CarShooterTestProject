using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Behaviour
{
    public class WanderState : IBehaviourState
    {
        private EnemyMovementController _movementController;
        private Transform _myTransform;
    
        private Vector3 _currentTarget;
        private WanderStateContext _stateContext;

        public WanderState(WanderStateContext context, Transform myTransform, EnemyMovementController movementController)
        {
            _stateContext = context;
            _myTransform = myTransform;
            _movementController = movementController;
        }

        public void Enter()
        {
            ChooseNewWanderPosition();
        }

        public void Update()
        {
            if ((_myTransform.position - _currentTarget).sqrMagnitude <= 
                _stateContext.DestinationCheckDistance * _stateContext.DestinationCheckDistance)
            {
                ChooseNewWanderPosition();
            }
        }

        private void ChooseNewWanderPosition()
        {
            Vector3 randomOffset = Random.insideUnitSphere * _stateContext.WanderRange;
            randomOffset.y = 0;
            _currentTarget = _myTransform.position + randomOffset;
            _currentTarget.x = Mathf.Clamp(_currentTarget.x, -_stateContext.RoadWidth, _stateContext.RoadWidth);
            _movementController.SetSpeedMultiplier(Random.Range(_stateContext.WanderSpeedMultiplier.x, _stateContext.WanderSpeedMultiplier.y));
            _movementController.SetDestination(_currentTarget, true);
        }

        public void Exit()
        {
        }
    }
}
