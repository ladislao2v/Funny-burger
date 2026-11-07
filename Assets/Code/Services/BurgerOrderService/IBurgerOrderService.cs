using System;
using Code.BurgerPlate;
using Code.Configs;

namespace Code.Services.BurgerOrderService
{
    public interface IBurgerOrderService
    {
        bool HasOrder { get; }
        bool HasTimer { get; }
        float CookTime { get; }

        event Action<RecipeConfig> Ordered;
        event Action Failed;
        event Action OrderPassed;

        void Order(RecipeConfig recipe, bool withTimer = true);
        bool TryPassOrder(IBurgerPlate plate);
        void CancelOrder();
    }
}