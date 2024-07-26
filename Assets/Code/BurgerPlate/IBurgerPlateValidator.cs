using System.Collections.Generic;
using Code.Ingredients;

namespace Code.BurgerPlate
{
    public interface IBurgerPlateValidator
    {
        public bool Validate(IngredientType ingredient);
    }
}