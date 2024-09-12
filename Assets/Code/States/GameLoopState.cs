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
        private readonly IClientsService _clientsService;
        private readonly IRecipeShopService _recipeShopService;

        private bool _isWorking = true;

        public GameLoopState(IInput input, IRecipeService recipeService, IGameDataService gameDataService, 
        IBurgerOrderService orderService, IClientsService clientsService, IRecipeShopService recipeShopService)
        {
            _input = input;
            _recipeService = recipeService;
            _gameDataService = gameDataService;
            _orderService = orderService;
            _clientsService = clientsService;
            _recipeShopService = recipeShopService;
        }
        
        public async void Enter()
        {
            await _clientsService.LoadClients();
            
            _input.Enable();
            _orderService.OrderPassed += _clientsService.ReturnClient;
            _orderService.OrderPassed += _gameDataService.SaveData;
            _orderService.Failed += _clientsService.ReturnClient;

            _recipeShopService.Updated += _gameDataService.SaveData;

            Order();
        }

        private async void Order()
        {
            while (_isWorking)
            {
                _clientsService.SendClient();

                await UniTask.WaitUntil(() => _clientsService.IsSend);
                
                RecipeConfig recipe = _recipeService.GetNextRecipe();
                _orderService.Order(recipe);
                
                await UniTask.WaitUntil(() => _clientsService.IsSend == false);
            }
        }

        public void Exit()
        {
            _isWorking = false;
            _input.Disable();
            _orderService.OrderPassed -= _clientsService.ReturnClient;
            _orderService.OrderPassed -= _gameDataService.SaveData;
            _orderService.Failed -= _clientsService.ReturnClient;
            
            _recipeShopService.Updated -= _gameDataService.SaveData;
        }
    }
}