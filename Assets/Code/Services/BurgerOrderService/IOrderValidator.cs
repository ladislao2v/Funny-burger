using System.Collections.Generic;
using Code.Configs;
using Code.Goods;

namespace Code.Services.BurgerOrderService
{
    public interface IOrderValidator
    {
        bool Validate(RecipeConfig currentOrder, IEnumerable<IngredientType> plateIngredients);
    }
}