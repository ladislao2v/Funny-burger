using System.Collections.Generic;
using System.Linq;
using Code.Goods;
using Code.Recipes;

namespace Code.Services.BurgerOrderService
{
    public class OrderValidator : IOrderValidator
    {
        public bool Validate(Recipe currentOrder, IEnumerable<IngredientType> plateIngredients)
        {
            var recipeIngredients = currentOrder
                .Burger
                .Select(x => x.Type);

            return recipeIngredients.SequenceEqual(plateIngredients);
        }
    }
}