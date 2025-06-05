
namespace Enemy.Behaviour
{
    public class EnemyBehaviourStateMachine
    {
        private IBehaviourState _currentState;

        public void Update()
        {
            _currentState?.Update();
        }
    
        public void SetState(IBehaviourState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}
