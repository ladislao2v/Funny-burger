using Code.Services.ClientsService;
using Code.Services.GameDataService;
using Code.Services.Input;
using Code.Services.LevelRewardService;
using Code.Services.ShopService;
using Code.Services.TutorialService;
using Cysharp.Threading.Tasks;
using Plugins.StateMachine.Core.Interfaces;

namespace Code.States
{
    public sealed class TutorialState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IInput _input;
        private readonly ITutorialServiceProvider _tutorialServiceProvider;
        private readonly IClientsServiceProvider _clientsServiceProvider;
        private readonly ITutorialProgress _tutorialProgress;
        private readonly ILevelRewardService _levelRewardService;
        private readonly IGameDataService _gameDataService;
        private readonly IShopService _shopService;
        private readonly ILocationsShopService _locationsShopService;

        public TutorialState(IStateMachine stateMachine, IInput input,
            ITutorialServiceProvider tutorialServiceProvider, IClientsServiceProvider clientsServiceProvider,
            ITutorialProgress tutorialProgress, ILevelRewardService levelRewardService,
            IGameDataService gameDataService, IShopService shopService,
            ILocationsShopService locationsShopService)
        {
            _stateMachine = stateMachine;
            _input = input;
            _tutorialServiceProvider = tutorialServiceProvider;
            _clientsServiceProvider = clientsServiceProvider;
            _tutorialProgress = tutorialProgress;
            _levelRewardService = levelRewardService;
            _gameDataService = gameDataService;
            _shopService = shopService;
            _locationsShopService = locationsShopService;
        }

        public async void Enter()
        {
            await UniTask.WaitUntil(() =>
                _tutorialServiceProvider.TutorialService != null &&
                _clientsServiceProvider.ClientsService != null);

            GiveInitialReward();

            _input.Enable();
            _input.ShowHint();
            _shopService.Updated += _gameDataService.SaveData;
            _locationsShopService.Updated += _gameDataService.SaveData;

            await _tutorialServiceProvider.TutorialService.Run();
            await UniTask.WaitUntil(() => _clientsServiceProvider.ClientsService.IsSend == false);

            _stateMachine.EnterState<GameLoopState>();
        }

        public void Exit()
        {
            _input.Disable();
            _shopService.Updated -= _gameDataService.SaveData;
            _locationsShopService.Updated -= _gameDataService.SaveData;
        }

        private void GiveInitialReward()
        {
            if (_tutorialProgress.IsStarted)
                return;

            _tutorialProgress.MarkStarted();
            _levelRewardService.GiveInitialReward();
            _gameDataService.SaveData();
        }
    }
}
