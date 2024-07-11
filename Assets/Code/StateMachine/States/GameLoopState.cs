using Code.Services.Input;
using Code.StateMachine.Core.Interfaces;
using UnityEngine;

namespace Code.StateMachine.States
{
    public sealed class GameLoopState : IState
    {
        private readonly IInput _input;

        public GameLoopState(IInput input)
        {
            _input = input;
        }
        
        public void Enter()
        {
            _input.Enable();
        }

        public void Exit()
        {
            _input.Disable();
        }
    }
}