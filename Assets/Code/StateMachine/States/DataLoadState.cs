using Code.Services.GameDataService;
using Plugins.StateMachine.Core.Interfaces;
using UnityEngine;

namespace Code.StateMachine.States
{
    public sealed class DataLoadState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IGameDataService _gameDataService;
        private readonly ISavable[] _savables;

        public DataLoadState(IStateMachine stateMachine, IGameDataService gameDataService, ISavable[] savables)
        {
            _stateMachine = stateMachine;
            _gameDataService = gameDataService;
            _savables = savables;
        }
        public void Enter()
        {
            RegisterSavables();
            
            _gameDataService.LoadData();
            _stateMachine.EnterState<GameLoopState>();
        }

        private void RegisterSavables()
        {
            foreach (var savables in _savables)
                _gameDataService.Add(savables, savables.SaveKey);
        }
    }
}