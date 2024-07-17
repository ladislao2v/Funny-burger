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
        [SerializeField] private Vector3 _offsetBetweenIngredients = new Vector3(0, 1, 0);

        private Stack<Ingredient> _ingredients;
        private IBurgerPlate _burgerPlate;
        private IIngredientFactory _factory;

        [Inject]
        private void Construct(IPlayer player, IIngredientFactory factory)
        {
            _burgerPlate = player.BurgerPlate;
            _factory = factory;
        }

        private void OnEnable()
        {
            _burgerPlate.IngredientAdded += OnAdded;
            _burgerPlate.Cleared += OnCleared;
        }

        private void OnDisable()
        {
            _burgerPlate.IngredientAdded -= OnAdded;
            _burgerPlate.Cleared -= OnCleared;
        }

        private async void OnAdded(IngredientType ingredientType)
        {
            Ingredient ingredient = await _factory.Create(ingredientType);
            
            ingredient.gameObject.SetActive(true);
            ingredient.transform.SetParent(_container);
            ingredient.transform.position = GetIngredientPlacementPosition();
            
            _ingredients.Push(ingredient);
        }

        private void OnCleared()
        {
            foreach (var ingredient in _ingredients)
                Destroy(ingredient);
            
            _ingredients.Clear();
        }

        private Vector3 GetIngredientPlacementPosition()
        {
            Vector3 lastPosition;

            if (_ingredients.TryPeek(out Ingredient last))
                lastPosition = last.transform.localPosition;
            else
                lastPosition = _container.localPosition;

            var newPosition = lastPosition + _offsetBetweenIngredients;
            
            return newPosition;
        }
    }
}