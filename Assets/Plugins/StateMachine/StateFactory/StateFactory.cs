using System;
using Plugins.StateMachine.Core.Interfaces;
using Zenject;

namespace Plugins.StateMachine.StateFactory
{
    public sealed class StateFactory : IStateFactory
    {
        private readonly IInstantiator _container;

        public StateFactory(IInstantiator container) => 
            _container = container;

        public IState Create<TState>() where TState : class, IState => 
            _container.Instantiate<TState>();

        public IState Create(Type type)
        {
            if (typeof(IState).IsAssignableFrom(type) == false)
                throw new ArgumentException(nameof(type));

            return (IState) _container.Instantiate(type);
        }
    }
}