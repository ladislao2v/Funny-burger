using Code.Configs;
using Code.Services.BurgerOrderService;
using Code.Services.ClientsService;
using Code.Services.Input;
using Code.Services.RecipeService;
using Cysharp.Threading.Tasks;
using Plugins.StateMachine.Core.Interfaces;

namespace Code.StateMachine.States
{
    public sealed class GameLoopState : IState
    {
        private readonly IInput _input;
        private readonly IRecipeService _recipeService;
        private readonly IBurgerOrderService _orderService;
        private readonly IClientsService _clientsService;
        
        private bool _isWorking = true;

        public GameLoopState(IInput input, IRecipeService recipeService, 
            IBurgerOrderService orderService, IClientsService clientsService)
        {
            _input = input;
            _recipeService = recipeService;
            _orderService = orderService;
            _clientsService = clientsService;
        }
        
        public void Enter()
        {
            _input.Enable();
            _orderService.OrderPassed += _clientsService.ReturnClient;
            _orderService.Failed += _clientsService.ReturnClient;

            Order();
        }

        private async void Order()
        {
            while (_isWorking)
            {
                _clientsService.SendClient();

                await UniTask.WaitWhile(() => _clientsService.IsSend);
                
                RecipeConfig recipe = _recipeService.GetNextRecipe();
                _orderService.Order(recipe);
                
                await UniTask.WaitWhile(() => _clientsService.IsSend == false);
            }
        }

        public void Exit()
        {
            _isWorking = false;
            _input.Disable();
            _orderService.OrderPassed -= _clientsService.ReturnClient;
            _orderService.Failed -= _clientsService.ReturnClient;
        }
    }
}