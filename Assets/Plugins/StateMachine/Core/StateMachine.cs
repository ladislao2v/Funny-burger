using System;
using System.Collections.Generic;
using Plugins.StateMachine.Core.Interfaces;
using Plugins.StateMachine.StateFactory;
using Zenject;

namespace Plugins.StateMachine.Core
{
    public sealed class StateMachine : IStateMachine, ITickable
    {
        private readonly IStateFactory _stateFactory;
        private readonly List<Transition> _transitions = new();
        
        private IState _currentState;

        public Transition CurrentTransition { get; private set; }

        public bool HasCurrentState => 
            _currentState != null;

        public StateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void EnterState<TState>() where TState : class, IState =>
            EnterState(typeof(TState));

        public void AddTransition<TStateTo>(Func<bool> condition) where TStateTo : class, IState
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));
            
            Type to = typeof(TStateTo);
            
            _transitions.Add(new Transition(to, condition));
        }

        public void AddTransition<TStateFrom, TStateTo>(Func<bool> condition) 
            where TStateFrom : class, IState
            where TStateTo : class, IState
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));

            Type from = typeof(TStateFrom);
            Type to = typeof(TStateTo);
            
            _transitions.Add(new Transition(from, to, condition));
        }

        public void Tick()
        {
            SetStateByTransition();
            
            if(_currentState is IUpdatableState state)
                state.Update();
        }
        
        private void EnterState(Type type)
        {
            _currentState?.Exit();
            _currentState = GetState(type);
            _currentState.Enter();
        }

        private void SetStateByTransition()
        {
            CurrentTransition = GetTransition();

            if (CurrentTransition == null)
                return;
            
            if (_currentState == CurrentTransition.To)
                return;
            
            EnterState(CurrentTransition.To.GetType());
        }

        private Transition GetTransition()
        {
            for (var i = 0; i < _transitions.Count; i++)
            {
                if (_transitions[i].From != null && _transitions[i].From != _currentState)
                    continue;

                if (_transitions[i].Condition.Invoke())
                    return _transitions[i];
            }

            return null;
        }
        
        
        private IState GetState(Type type) => 
            _stateFactory.Create(type);
    }
}