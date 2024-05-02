using System;
using Code.StateMachine.Core.Interfaces;

namespace Code.Services.Factories.StateFactory
{
    public interface IStateFactory
    {
        IState Create<TState>() where TState : class, IState;
        IState Create(Type type);
    }
}