using System;
using System.Collections.Generic;
using System.Linq;
using Code.Ingredients;
using ModestTree;

namespace Code.BurgerPlate
{
    public sealed class Plate : IBurgerPlate
    {
        private readonly Stack<IngredientType> _ingredients = new ();

        public IEnumerable<IngredientType> Ingredients => _ingredients;
        public bool IsEmpty => _ingredients.IsEmpty();

        public event Action<IngredientType> IngredientAdded;
        public event Action<IEnumerable<IngredientType>> IngredientsAdded;
        public event Action Cleared;

        public void Add(IngredientType ingredientType)
        {
            _ingredients.Push(ingredientType);

            IngredientAdded?.Invoke(ingredientType);
        }

        public void AddRange(IEnumerable<IngredientType> ingredients)
        {
            var reversed = ingredients
                .Reverse();
            
            foreach (var ingredient in reversed)
                _ingredients.Push(ingredient);
            
            IngredientsAdded?.Invoke(reversed);
        }

        public bool Contains(IngredientType ingredient) => 
            _ingredients.Contains(ingredient);

        public void Clear()
        {
            _ingredients.Clear();
            
            Cleared?.Invoke();
        }
    }
}