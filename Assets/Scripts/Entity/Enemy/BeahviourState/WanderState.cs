
using Zenject;

public class WanderState : IBehaviourState
{
    private EnemyMovementController _movementController;

    public WanderState(EnemyMovementController movementController)
    {
        _movementController = movementController;
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
