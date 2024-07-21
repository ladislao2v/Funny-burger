using System.Collections.Generic;
using Code.Goods;

namespace Code.BurgerPlate
{
    public class TablePlateView : PlateView
    {
        private IBurgerPlate _burgerPlate;

        public void Construct(IBurgerPlate burgerPlate)
        {
            _burgerPlate = burgerPlate;

            _burgerPlate.IngredientsAdded += ShowBurger;
            _burgerPlate.Cleared += ClearPlate;
        }

        public async void ShowBurger(IEnumerable<IngredientType> ingredients) => 
            await AddIngredientsView(ingredients);

        private void OnDestroy()
        {
            _burgerPlate.IngredientsAdded -= ShowBurger;
            _burgerPlate.Cleared -= ClearPlate;
        }
    }
}