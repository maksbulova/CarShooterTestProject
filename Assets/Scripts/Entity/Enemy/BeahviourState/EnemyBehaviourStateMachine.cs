
public class EnemyBehaviourStateMachine
{
    private IBehaviourState _currentState;

    public void SetState(IBehaviourState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
}
