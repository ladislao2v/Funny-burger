using Code.BurgerPlate;
using Code.Recipes;
using UniRx;

namespace Code.Services.BurgerOrderService
{
    public interface IBurgerOrderService
    {
        IReadOnlyReactiveProperty<Recipe> CurrentOrder { get; }

        void Order(Recipe recipe);
        bool TryPassOrder(IBurgerPlate plate);
    }
}