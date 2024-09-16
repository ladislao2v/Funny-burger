using System;
using Code.BurgerPlate;
using Code.Configs;

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