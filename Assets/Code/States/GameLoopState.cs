using Code.Configs;
using Code.Services.BurgerOrderService;
using Code.Services.ClientsService;
using Code.Services.GameDataService;
using Code.Services.Input;
using Code.Services.RecipeService;
using Code.Services.ShopService;
using Cysharp.Threading.Tasks;
using Plugins.StateMachine.Core.Interfaces;

namespace Code.States
{
    public sealed class GameLoopState : IState
    {
        private readonly IInput _input;
        private readonly IRecipeService _recipeService;
        private readonly IGameDataService _gameDataService;
        private readonly IBurgerOrderService _orderService;
        private readonly IClientsServiceProvider _clientsServiceProvider;
        private readonly IShopService _shopService;

        private bool _isWorking = true;

        public GameLoopState(IInput input, IRecipeService recipeService, 
            IGameDataService gameDataService, IBurgerOrderService orderService, 
            IClientsServiceProvider clientsServiceProvider, 
            IShopService shopService)
        {
            _input = input;
            _recipeService = recipeService;
            _gameDataService = gameDataService;
            _orderService = orderService;
            _clientsServiceProvider = clientsServiceProvider;
            _shopService = shopService;
        }
        
        public async void Enter()
        {
            await UniTask.WaitUntil(() => _clientsServiceProvider.ClientsService != null);
            await _clientsServiceProvider.ClientsService.LoadClients();
            
            _input.Enable();
            _orderService.OrderPassed += _clientsServiceProvider.ClientsService.SendClientAway;
            _orderService.OrderPassed += _gameDataService.SaveData;
            _orderService.Failed += _clientsServiceProvider.ClientsService.SendClientAway;

            _shopService.Updated += _gameDataService.SaveData;

            Order();
        }

        private async void Order()
        {
            while (_isWorking)
            {
                _clientsServiceProvider.ClientsService.SendClientToWindow();

                await UniTask.WaitUntil(() => _clientsServiceProvider.ClientsService.IsSend);
                
                RecipeConfig recipe = _recipeService.GetNextRecipe();
                _orderService.Order(recipe);
                
                await UniTask.WaitUntil(() => _clientsServiceProvider.ClientsService.IsSend == false);
            }
        }

        public void Exit()
        {
            _isWorking = false;
            _input.Disable();
            _orderService.OrderPassed -= _clientsServiceProvider.ClientsService.SendClientAway;
            _orderService.OrderPassed -= _gameDataService.SaveData;
            _orderService.Failed -= _clientsServiceProvider.ClientsService.SendClientAway;
            
            _shopService.Updated -= _gameDataService.SaveData;
        }
    }
}