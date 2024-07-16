namespace Plugins.StateMachine.Core.Interfaces
{
    public interface IState
    {
        void Enter();
        void Exit() { }
    }
}