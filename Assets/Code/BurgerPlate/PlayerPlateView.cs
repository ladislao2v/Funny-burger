using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Goods;
using Code.Ingredients;
using Code.Services.Factories.IngredientFactory;
using Code.Units;
using UnityEngine;
using Zenject;

namespace Code.BurgerPlate
{
    public sealed class PlayerPlateView : PlateView
    {
        private IBurgerPlate _burgerPlate;

        [Inject]
        private void Construct(IPlayer player)
        {
            _burgerPlate = player.BurgerPlate;
            
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

        private async void OnRangeAdded(IngredientType[] ingredients)
        {
            await AddIngredientsView(ingredients);
        }

        private void OnCleared() => 
            ClearPlate();
    }
}