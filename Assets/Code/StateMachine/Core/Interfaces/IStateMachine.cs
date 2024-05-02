using System;
using Code.Services.Factories.StateFactory;

namespace Code.StateMachine.Core.Interfaces
{
    public interface IStateMachine
    {
        bool HasCurrentState { get; }
        Transition CurrentTransition { get; }

        void EnterState<TState>() where TState : class, IState;
        void AddTransition<TStateFrom, TStateTo>(Func<bool> condition)
            where TStateFrom : class, IState
            where TStateTo : class, IState;
        void AddTransition<TStateTo>(Func<bool> condition) where TStateTo : class, IState;
        void Update();
    }
}