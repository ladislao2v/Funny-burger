using Code.Ingredients;
using static Code.Ingredients.IngredientType;

namespace Code.BurgerPlate
{
    public class BurgerPlateValidator : IBurgerPlateValidator
    {
        private readonly IBurgerPlate _burgerPlate;

        public BurgerPlateValidator(IBurgerPlate burgerPlate)
        {
            _burgerPlate = burgerPlate;
        }
        
        public bool Validate(IngredientType ingredient)
        {
            if (_burgerPlate.IsEmpty && ingredient != BottomBun)
                return false;

            if (_burgerPlate.Contains(TopBun))
                return false;

            return true;
        }
    }
}