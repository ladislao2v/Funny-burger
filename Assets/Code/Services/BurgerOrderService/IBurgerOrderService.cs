using System;
using Code.BurgerPlate;
using Code.Configs;
using Code.Services.BurgerOrderService;
using Code.Services.RecipeService;
using UniRx;

namespace Code.Services.BurgerOrderService
{
    public interface IBurgerOrderService
    {
        bool HasOrder { get; }
        
        event Action<RecipeConfig> Ordered;
        event Action Failed;
        event Action OrderPassed;

        void Order(RecipeConfig recipe);
        bool TryPassOrder(IBurgerPlate plate);
        void CancelOrder();
    }
}