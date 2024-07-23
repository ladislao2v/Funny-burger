using Code.BurgerPlate;
<<<<<<< HEAD
using Code.Configs;
using Code.Services.RecipeService;
=======
using Code.Recipes;
using UniRx;
>>>>>>> 3f2981a6ce365d14b31d0e298b0e3d3d8fd23979

namespace Code.Services.BurgerOrderService
{
    public interface IBurgerOrderService
    {
<<<<<<< HEAD
        public RecipeConfig CurrentOrder { get; } 
        
        public bool TryPassOrder(IBurgerPlate plate);
    }

    public class BurgerOrderService : IBurgerOrderService
    {
        private readonly IRecipeService _recipeService;
        private readonly IOrderValidator _orderValidator = new OrderValidator();

        public RecipeConfig CurrentOrder { get; }

        public BurgerOrderService(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public bool TryPassOrder(IBurgerPlate plate)
        {
            if (!_orderValidator.Validate(CurrentOrder, plate.Ingredients))
                return false;

            return true;
        }
=======
        IReadOnlyReactiveProperty<Recipe> CurrentOrder { get; }

        void Order(Recipe recipe);
        bool TryPassOrder(IBurgerPlate plate);
>>>>>>> 3f2981a6ce365d14b31d0e298b0e3d3d8fd23979
    }
}