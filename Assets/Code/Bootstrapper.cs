using Code.States;
using Plugins.StateMachine.Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Code
{
    public class Bootstrapper : MonoBehaviour
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void Construct(IStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        private void Awake() => 
            _stateMachine.EnterState<BootState>();
    }
}
