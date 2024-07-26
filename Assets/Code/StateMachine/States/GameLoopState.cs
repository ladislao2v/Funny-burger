using Code.Configs;
using Code.Services.BurgerOrderService;
using Code.Services.Input;
using Code.Services.RecipeService;
using Cysharp.Threading.Tasks;
using Plugins.StateMachine.Core.Interfaces;
using UnityEngine;

namespace Code.StateMachine.States
{
    public sealed class GameLoopState : IState
    {
        private readonly IInput _input;
        private readonly IRecipeService _recipeService;
        private readonly IBurgerOrderService _orderService;

        public GameLoopState(IInput input, IRecipeService recipeService, IBurgerOrderService orderService)
        {
            _input = input;
            _recipeService = recipeService;
            _orderService = orderService;
        }
        
        public void Enter()
        {
            _input.Enable();

            Order();
        }

        private void Order()
        {
            RecipeConfig recipe = _recipeService.GetNextRecipe();
            _orderService.Order(recipe);
        }

        public void Exit()
        {
            _input.Disable();
        }
    }
}