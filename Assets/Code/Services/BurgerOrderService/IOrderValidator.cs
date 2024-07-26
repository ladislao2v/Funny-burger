using System.Collections.Generic;
using Code.Configs;
using Code.Ingredients;

namespace Code.Services.BurgerOrderService
{
    public interface IOrderValidator
    {
        bool Validate(RecipeConfig currentOrder, IEnumerable<IngredientType> plateIngredients);
    }
}