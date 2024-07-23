using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Goods;

namespace Code.Services.BurgerOrderService
{
    public class OrderValidator : IOrderValidator
    {
        public bool Validate(RecipeConfig currentOrder, IEnumerable<IngredientType> plateIngredients)
        {
            var recipeIngredients = currentOrder
                .Burger
                .Select(x => x.Type);

            return recipeIngredients.SequenceEqual(plateIngredients);
        }
    }
}