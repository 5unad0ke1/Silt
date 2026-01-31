namespace Silt.StateMachine
{
    public abstract class StateBase<TController, TState> : IState
    where TController : class
    where TState : StateBase<TController, TState>, new()
    {
        protected StateMachine _machine;
        protected TController _controller;
        public TController Controller => _controller;
        public static TState CreateInstance(TController controller)
         => new()
         {
             _controller = controller
         };
        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();

        public void SetMachine(StateMachine machine)
        {
            if (_machine is not null)
                return;
            _machine = machine;
        }
    }
}