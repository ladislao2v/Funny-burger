using System.Collections.Generic;
using Code.Goods;

namespace Code.BurgerPlate
{
    public interface IBurgerPlateValidator
    {
        public bool Validate(IngredientType ingredient);
    }
}