using Code.BurgerPlate;
using Code.Configs;
using Code.Services.BurgerOrderService;
using Code.Services.RecipeService;
using UniRx;

namespace Code.Services.BurgerOrderService
{
    public interface IBurgerOrderService
    {
        IReadOnlyReactiveProperty<RecipeConfig> CurrentOrder { get; }

        void Order(RecipeConfig recipe);
        bool TryPassOrder(IBurgerPlate plate);
    }
}