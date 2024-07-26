using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.Ingredients;
using Code.Services.Factories.IngredientFactory;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Code.BurgerPlate
{
    public abstract class PlateView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Vector3 _offsetBetweenIngredients;

        private readonly Stack<Ingredient> _ingredients = new();
        private IIngredientFactory _factory;

        [Inject]
        private void Construct(IIngredientFactory factory)
        {
            _factory = factory;
        }
        
        protected async Task AddIngredientView(IngredientType ingredientType)
        {
            Ingredient ingredient = await _factory
                .Create(ingredientType);

            SetupIngredient(ingredient);

            _ingredients.Push(ingredient);
        }
        
        protected async Task AddIngredientsView(IEnumerable<IngredientType> ingredients)
        {
            foreach (var ingredient in ingredients)
                await AddIngredientView(ingredient);
        }

        protected void ClearPlate()
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