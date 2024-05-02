using Code.StateMachine.Core.Interfaces;
using UnityEngine;

namespace Code.StateMachine.States
{
    public class MenuState : IState
    {
        public void Enter()
        {
            Debug.Log("MenuState enter");
        }

        public void Exit()
        {
            Debug.Log("MenuState exit");
        }
    }
}