using System.Collections.Generic;
using Code.Goods;
using Code.Recipes;

namespace Code.Services.BurgerOrderService
{
    public interface IOrderValidator
    {
        bool Validate(Recipe currentOrder, IReadOnlyCollection<IngredientType> plateIngredients);
    }
}