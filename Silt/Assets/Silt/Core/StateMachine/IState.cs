namespace Silt.Core
{
    public interface IState
    {
        void Enter();
        void Execute();
        void Exit();
        void SetMachine(StateMachine machine);
    }
}