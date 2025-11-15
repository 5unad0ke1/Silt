namespace Silt.Core
{
    public sealed class StateMachine
    {
        private IState _currentState;
        public IState CurrentState => _currentState;

        public void ChangeState(IState nextState)
        {
            if (nextState is null)
                return;
            nextState.SetMachine(this);

            if (_currentState is not null)
                _currentState.Exit();

            nextState.Enter();

            _currentState = nextState;
        }
        public void Execute()
        {
            _currentState.Execute();
        }
    }
}