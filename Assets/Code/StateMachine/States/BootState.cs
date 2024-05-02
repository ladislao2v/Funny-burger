using Code.Services.Factories.StateFactory;
using Code.Services.SceneLoader;
using Code.StateMachine.Core.Interfaces;
using UnityEngine;

namespace Code.StateMachine.States
{
    public class BootState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public BootState(IStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public async void Enter()
        {
            Debug.Log("BootState enter");
            
            _sceneLoader
                .LoadScene(SceneNames.Game, () => _stateMachine.EnterState<MenuState>());
        }

        public async void Exit()
        {
            Debug.Log("BootState exit");
        }
    }
}