using System;
using System.Collections.Generic;
using Code.Goods;
using ModestTree;
using UnityEngine;

namespace Code.BurgerPlate
{
    public sealed class Plate : IBurgerPlate
    {
        private readonly Stack<IngredientType> _ingredients = new ();

        public bool IsEmpty => _ingredients.IsEmpty();

        public event Action<IngredientType> IngredientAdded; 
        public event Action Cleared;

        public void Add(IngredientType ingredientType)
        {
            _ingredients.Push(ingredientType);

            IngredientAdded?.Invoke(ingredientType);
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