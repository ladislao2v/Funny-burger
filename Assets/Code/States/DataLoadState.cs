using Code.Services.GameDataService;
using Code.Services.SceneLoader;
using Plugins.StateMachine.Core.Interfaces;

namespace Code.States
{
    public sealed class DataLoadState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IGameDataService _gameDataService;
        private readonly ISceneLoader _sceneLoader;
        private readonly ISavable[] _savables;

        public DataLoadState(IStateMachine stateMachine, IGameDataService gameDataService, 
            ISceneLoader sceneLoader,ISavable[] savables)
        {
            _stateMachine = stateMachine;
            _gameDataService = gameDataService;
            _sceneLoader = sceneLoader;
            _savables = savables;
        }
        public void Enter()
        {
            RegisterSavables();
            
            _gameDataService.LoadData();
            _sceneLoader.LoadScene(SceneNames.Game, _stateMachine.EnterState<GameLoopState>);
        }

        private void RegisterSavables()
        {
            foreach (var savables in _savables)
                _gameDataService.Add(savables, savables.SaveKey);
        }
    }
}