using Code.Services.GameDataService;
using Code.Services.LevelRewardService;
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
        private readonly ILevelRewardService _levelRewardService;

        public DataLoadState(IStateMachine stateMachine, IGameDataService gameDataService, 
            ISceneLoader sceneLoader, ISavable[] savables, ILevelRewardService levelRewardService)
        {
            _stateMachine = stateMachine;
            _gameDataService = gameDataService;
            _sceneLoader = sceneLoader;
            _savables = savables;
            _levelRewardService = levelRewardService;
        }
        public void Enter()
        {
            RegisterSavables();
            
            _gameDataService.LoadData();
            _levelRewardService.RefreshNextReward();
            _sceneLoader.LoadScene(SceneNames.Game, _stateMachine.EnterState<GameLoopState>);
        }

        private void RegisterSavables()
        {
            foreach (var savables in _savables)
                _gameDataService.Add(savables, savables.SaveKey);
        }
    }
}