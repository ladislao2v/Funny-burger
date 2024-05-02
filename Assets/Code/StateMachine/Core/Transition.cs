using System;
using Code.StateMachine.Core.Interfaces;

namespace Code.StateMachine.Core
{
    public class Transition
    {
        public readonly Type From;
        public readonly Type To;
        public readonly Func<bool> Condition;

        public Transition(Type to, Func<bool> condition)
        {
            From = null;
            To = to;
            Condition = condition;
        }

        public Transition(Type from, Type to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}