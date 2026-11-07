using System.Collections.Generic;
using System.Linq;
using Code.BurgerPlate;
using Code.Configs;
using Code.Ingredients;
using Code.Effects.Box;
using Code.Services.BurgerOrderService;
using Code.Services.ClientsService;
using Code.Services.ConfigProvider;
using Code.Services.GameDataService;
using Code.Services.LocalizationService;
using Code.Services.PopupService;
using Code.Services.PurchasedBoxesService;
using Code.Services.TutorialService.Steps;
using Code.Triggers.Table;
using Code.Triggers.Trash;
using Code.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using static Code.Ingredients.IngredientType;

namespace Code.Services.TutorialService
{
    public sealed class TutorialService : MonoBehaviour, ITutorialService
    {
        [SerializeField] private TutorialArrowView _bottomBuns;
        [SerializeField] private TutorialArrowView _topBuns;
        [SerializeField] private TutorialArrowView _burgerBake;
        [SerializeField] private TutorialArrowView _orderWindow;
        [SerializeField] private TutorialArrowView _trash;
        [SerializeField] private TutorialArrowView _table;
        
        [Header("Triggers")]
        [SerializeField] private TrashTrigger _trashTrigger;
        [SerializeField] private TableTrigger _tableTrigger;

        private readonly List<ITutorialStep> _tutorialSteps = new();
        private readonly HashSet<ITutorialStep> _unsavedSteps = new();

        private ITutorialProgress _progress;
        private IGameDataService _gameDataService;
        private IConfigProvider _configProvider;
        private IPlayerProvider _playerProvider;
        private IClientsServiceProvider _clientsServiceProvider;
        private IBurgerOrderService _orderService;
        private IPurchasedBoxesService _purchasedBoxesService;
        private IPopupService _popupService;
        private IPopupContainerProvider _popupContainerProvider;
        private ILocalizationService _localizationService;
        private ITutorialIngredientFilter _ingredientFilter;

        private TutorialConfig _config;
        private IBurgerPlate _plate;
        private IPlayer _player;
        private Vector3 _chefStartPosition;
        private ITutorialStep _currentStep;
        private int _currentIndex;
        private int _sendClientIndex;
        private int _takeBottomBunIndex;
        private int _passOrderIndex;
        private int _tableGroupIndex;
        private int _useTableIndex;
        private UniTaskCompletionSource _completion;

        [Inject]
        private void Construct(ITutorialProgress progress, IGameDataService gameDataService,
            IConfigProvider configProvider, IPlayerProvider playerProvider,
            IClientsServiceProvider clientsServiceProvider, IBurgerOrderService orderService,
            IPurchasedBoxesService purchasedBoxesService,
            IPopupService popupService, IPopupContainerProvider popupContainerProvider,
            ILocalizationService localizationService, ITutorialIngredientFilter ingredientFilter)
        {
            _progress = progress;
            _gameDataService = gameDataService;
            _configProvider = configProvider;
            _playerProvider = playerProvider;
            _clientsServiceProvider = clientsServiceProvider;
            _orderService = orderService;
            _purchasedBoxesService = purchasedBoxesService;
            _popupService = popupService;
            _popupContainerProvider = popupContainerProvider;
            _localizationService = localizationService;
            _ingredientFilter = ingredientFilter;
        }

        public UniTask Run()
        {
            if (_progress.IsCompleted)
                return UniTask.CompletedTask;

            if (_completion != null)
                return _completion.Task;

            _completion = new UniTaskCompletionSource();
            _config = _configProvider.TutorialConfig;
            _player = _playerProvider.Player;
            _plate = _player.Plate;
            _chefStartPosition = _player.Transform.position;

            CreateSteps();

            _orderService.Failed += OnOrderFailed;
            _plate.Cleared += OnPlateCleared;
            _trashTrigger.InteractionStarted += OnTrashUsed;
            _ingredientFilter.Enable(GetRecipeIngredients());

            StartStep(Mathf.Clamp(_progress.CompletedSteps, 0, _tutorialSteps.Count));

            return _completion.Task;
        }

        private void OnDestroy()
        {
            EndCurrentStep();
            Unsubscribe();
            _ingredientFilter?.Disable();
        }

        private void CreateSteps()
        {
            IClientsService clientsService = _clientsServiceProvider.ClientsService;

            var welcomeHint = CreateHintStep(_config.WelcomeHint);
            var buyBottomBunBox = new BuyBoxStep(_bottomBuns, _purchasedBoxesService, BottomBun);
            var buyTopBunBox = new BuyBoxStep(_topBuns, _purchasedBoxesService, TopBun);
            
            var sendClient = new SendTutorialClientStep(clientsService, _orderService, _config.Recipe);
            var takeBottomBun = new TakeIngredientStep(_bottomBuns, _plate, BottomBun);
            var fryBurger = new TakeIngredientStep(_burgerBake, _plate, Burger);
            var takeTopBun = new TakeIngredientStep(_topBuns, _plate, TopBun);
            var passOrder = new PassOrderStep(_orderWindow, _orderService, clientsService);

            var returnBeforeTrash = CreateReturnChefStep();
            var spawnWrongBurger = new SpawnBurgerStep(_plate, _config.WrongBurger);
            var trashHint = CreateHintStep(_config.TrashHint);
            var useTrash = new UseTriggerStep(_trash, _trashTrigger, _player);

            var returnBeforeTable = CreateReturnChefStep();
            var spawnHamburger = new SpawnBurgerStep(_plate, GetRecipeIngredients());
            var tableHint = CreateHintStep(_config.TableHint);
            var useTable = new UseTriggerStep(_table, _tableTrigger, _player);

            var returnBeforeLoyalty = CreateReturnChefStep();

            var loyaltyHint = CreateHintStep(_config.LoyaltyHint);
            var letsGoHint = CreateHintStep(_config.LetsGoHint);

            _tutorialSteps.Clear();
            _tutorialSteps.AddRange(new ITutorialStep[]
            {
                welcomeHint,
                buyBottomBunBox,
                buyTopBunBox,
                sendClient,
                takeBottomBun,
                fryBurger,
                takeTopBun,
                passOrder,
                returnBeforeTrash,
                spawnWrongBurger,
                trashHint,
                useTrash,
                returnBeforeTable,
                spawnHamburger,
                tableHint,
                useTable,
                returnBeforeLoyalty,
                loyaltyHint,
                letsGoHint,
            });

            _unsavedSteps.Clear();
            _unsavedSteps.UnionWith(new ITutorialStep[]
            {
                sendClient, takeBottomBun, fryBurger, takeTopBun,
                returnBeforeTrash, spawnWrongBurger, trashHint,
                returnBeforeTable, spawnHamburger, tableHint,
                returnBeforeLoyalty
            });

            _sendClientIndex = _tutorialSteps.IndexOf(sendClient);
            _takeBottomBunIndex = _tutorialSteps.IndexOf(takeBottomBun);
            _passOrderIndex = _tutorialSteps.IndexOf(passOrder);
            _tableGroupIndex = _tutorialSteps.IndexOf(returnBeforeTable);
            _useTableIndex = _tutorialSteps.IndexOf(useTable);
        }

        private ITutorialStep CreateReturnChefStep() =>
            new ReturnChefStep(_player, _chefStartPosition);

        private List<IngredientType> GetRecipeIngredients() => 
            _config.Recipe.Burger
                .Select(ingredient => ingredient.Type)
                .ToList();

        private ITutorialStep CreateHintStep(TutorialHintConfig hint) =>
            new HintPopupStep(_popupService, _popupContainerProvider, _localizationService, hint);

        private void StartStep(int index)
        {
            _currentIndex = index;

            if (index > _passOrderIndex)
                _ingredientFilter.Disable();

            if (index >= _tutorialSteps.Count)
            {
                Finish();
                return;
            }

            _currentStep = _tutorialSteps[index];
            _currentStep.Completed += OnStepCompleted;
            _currentStep.StartStep();
        }

        private void OnStepCompleted()
        {
            int completedIndex = _currentIndex;

            EndCurrentStep();
            SaveProgress(completedIndex);
            StartStep(completedIndex + 1);
        }

        private void RestartFrom(int index)
        {
            EndCurrentStep();
            StartStep(index);
        }

        private void EndCurrentStep()
        {
            if (_currentStep == null)
                return;

            _currentStep.Completed -= OnStepCompleted;
            _currentStep.EndStep();
            _currentStep = null;
        }

        private void OnOrderFailed()
        {
            if (_currentIndex <= _sendClientIndex || _currentIndex > _passOrderIndex)
                return;

            _orderService.Order(_config.Recipe, withTimer: false);

            RestartFrom(_takeBottomBunIndex);
        }

        private void OnPlateCleared()
        {
            if (_currentIndex <= _takeBottomBunIndex || _currentIndex > _passOrderIndex)
                return;

            RestartFrom(_takeBottomBunIndex);
        }

        private void OnTrashUsed()
        {
            if (_currentIndex != _useTableIndex)
                return;

            RestartFrom(_tableGroupIndex);
        }

        private void SaveProgress(int completedIndex)
        {
            if (_unsavedSteps.Contains(_tutorialSteps[completedIndex]))
                return;

            _progress.CompleteSteps(completedIndex + 1);
            _gameDataService.SaveData();
        }

        private void Finish()
        {
            Unsubscribe();

            _progress.Complete();
            _gameDataService.SaveData();

            _completion.TrySetResult();
        }

        private void Unsubscribe()
        {
            if (_completion == null)
                return;

            _orderService.Failed -= OnOrderFailed;
            _plate.Cleared -= OnPlateCleared;
            _trashTrigger.InteractionStarted -= OnTrashUsed;
        }
    }
}
