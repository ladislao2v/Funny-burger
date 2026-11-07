using Code.Constants;
using Code.Services.GameDataService;
using Code.Services.LevelRewardService;
using Code.Services.LevelService;
using Code.Services.PurchasedBoxesService;
using Code.Services.SceneLoader;
using Code.Services.TutorialService;
using Plugins.StateMachine.Core.Interfaces;
using static Code.Ingredients.IngredientType;

namespace Code.States
{
    public sealed class DataLoadState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IGameDataService _gameDataService;
        private readonly ISceneLoader _sceneLoader;
        private readonly ISavable[] _savables;
        private readonly ILevelRewardService _levelRewardService;
        private readonly ILevelService _levelService;
        private readonly ITutorialProgress _tutorialProgress;
        private readonly IPurchasedBoxesService _purchasedBoxesService;

        public DataLoadState(IStateMachine stateMachine, IGameDataService gameDataService,
            ISceneLoader sceneLoader, ISavable[] savables, ILevelRewardService levelRewardService,
            ILevelService levelService, ITutorialProgress tutorialProgress,
            IPurchasedBoxesService purchasedBoxesService)
        {
            _stateMachine = stateMachine;
            _gameDataService = gameDataService;
            _sceneLoader = sceneLoader;
            _savables = savables;
            _levelRewardService = levelRewardService;
            _levelService = levelService;
            _tutorialProgress = tutorialProgress;
            _purchasedBoxesService = purchasedBoxesService;
        }
        public void Enter()
        {
            RegisterSavables();

            _gameDataService.LoadData();
            SkipTutorialForExistingPlayer();
            _levelRewardService.RefreshNextReward();
            _sceneLoader.LoadScene(SceneNames.Location1, EnterGameState);
        }

        private void RegisterSavables()
        {
            foreach (var savables in _savables)
                _gameDataService.Add(savables, savables.SaveKey);
        }

        private void SkipTutorialForExistingPlayer()
        {
            if (_tutorialProgress.IsCompleted || _levelService.Current == GameplayConstants.InitialLevel)
                return;

            _tutorialProgress.Complete();
            _purchasedBoxesService.MarkPurchased(BottomBun);
            _purchasedBoxesService.MarkPurchased(TopBun);
            _gameDataService.SaveData();
        }

        private void EnterGameState()
        {
            if (_tutorialProgress.IsCompleted)
                _stateMachine.EnterState<GameLoopState>();
            else
                _stateMachine.EnterState<TutorialState>();
        }
    }
}
