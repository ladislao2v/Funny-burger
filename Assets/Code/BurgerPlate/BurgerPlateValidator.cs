
using System.Collections.Generic;
using System.Linq;
using Code.Goods;
using ModestTree;

namespace Code.BurgerPlate
{
    public class BurgerPlateValidator : IBurgerPlateValidator
    {
        public bool Validate(IReadOnlyCollection<IngredientType> ingredients, IngredientType ingredient)
        {
            if (ingredients.IsEmpty() && ingredient == IngredientType.BottomBun)
                return false;

            if (ingredient == IngredientType.BottomBun && ingredients.Contains(ingredient))
                return false;

            if (ingredient == IngredientType.TopBun && ingredients.Contains(ingredient))
                return false;

            return true;
        }
    }
}