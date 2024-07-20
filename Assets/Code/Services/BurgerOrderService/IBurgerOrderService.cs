using Code.BurgerPlate;
using Code.Recipes;
using Code.Services.RecipeService;

namespace Code.Services.BurgerOrderService
{
    public interface IBurgerOrderService
    {
        public Recipe CurrentOrder { get; } 
        
        public bool TryPassOrder(IBurgerPlate plate);
    }

    public class BurgerOrderService : IBurgerOrderService
    {
        private readonly IRecipeService _recipeService;
        private readonly IOrderValidator _orderValidator = new OrderValidator();

        public Recipe CurrentOrder { get; }

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
    }
}