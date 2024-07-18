using System;
using System.Collections.Generic;
using Code.Goods;
using Code.Ingredients;
using Code.Services.Factories.IngredientFactory;
using Code.Units;
using UnityEngine;
using Zenject;

namespace Code.BurgerPlate
{
    public sealed class PlateStackPresenter : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Vector3 _offsetBetweenIngredients;

        private readonly Stack<Ingredient> _ingredients = new();
        private IBurgerPlate _burgerPlate;
        private IIngredientFactory _factory;

        [Inject]
        private void Construct(IPlayer player, IIngredientFactory factory)
        {
            _burgerPlate = player.BurgerPlate;
            _factory = factory;
            
            _burgerPlate.IngredientAdded += OnAdded;
            _burgerPlate.Cleared += OnCleared;
        }

        private void OnDestroy()
        {
            _burgerPlate.IngredientAdded -= OnAdded;
            _burgerPlate.Cleared -= OnCleared;
        }

        private async void OnAdded(IngredientType ingredientType)
        {
            Ingredient ingredient = await _factory
                .Create(ingredientType);
            
            SetupIngredient(ingredient);

            _ingredients.Push(ingredient);
        }

        private void OnCleared()
        {
            foreach (var ingredient in _ingredients)
                Destroy(ingredient.gameObject);
            
            _ingredients.Clear();
        }

        private void SetupIngredient(Ingredient ingredient)
        {
            ingredient.gameObject.SetActive(true);
            ingredient.transform.SetParent(_container);
            ingredient.transform.localPosition = GetPosition();
        }

        private Vector3 GetPosition() =>
            _ingredients.Count * _offsetBetweenIngredients;
    }
}