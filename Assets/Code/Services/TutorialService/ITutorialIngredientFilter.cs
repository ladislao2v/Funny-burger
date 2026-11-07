using System.Collections.Generic;
using Code.BurgerPlate;
using Code.Ingredients;

namespace Code.Services.TutorialService
{
    public interface ITutorialIngredientFilter
    {
        bool CanTake(IBurgerPlate plate, IngredientType ingredient);
        void Enable(IEnumerable<IngredientType> burger);
        void Disable();
    }
}
