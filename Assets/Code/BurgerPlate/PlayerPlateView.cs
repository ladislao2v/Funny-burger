using System.Collections.Generic;
using Code.Ingredients;
using Code.Units;
using Zenject;

namespace Code.BurgerPlate
{
    public sealed class PlayerPlateView : PlateView
    {
        private IBurgerPlate _burgerPlate;

        [Inject]
        private void Construct(IPlayer player)
        {
            _burgerPlate = player.Plate;
            
            _burgerPlate.IngredientAdded += OnAdded;
            _burgerPlate.IngredientsAdded += OnRangeAdded; 
            _burgerPlate.Cleared += OnCleared;
        }

        private void OnDestroy()
        {
            _burgerPlate.IngredientAdded -= OnAdded;
            _burgerPlate.Cleared -= OnCleared;
        }

        private async void OnAdded(IngredientType ingredientType) => 
            await AddIngredientView(ingredientType);

        private async void OnRangeAdded(IEnumerable<IngredientType> ingredients) => 
            await AddIngredientsView(ingredients);

        private void OnCleared() => 
            ClearPlate();
    }
}