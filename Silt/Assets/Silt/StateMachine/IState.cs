namespace Silt.StateMachine
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
        void SetMachine(StateMachine machine);
    }
}