using System;
using System.Collections.Generic;
using Code.Goods;
using ModestTree;

namespace Code.BurgerPlate
{
    public sealed class Plate : IBurgerPlate
    {
        private readonly Stack<IngredientType> _ingredients = new ();
        private readonly IBurgerPlateValidator _validator = new BurgerPlateValidator();

        public bool IsEmpty => _ingredients.IsEmpty();

        public event Action<IngredientType> IngredientAdded; 
        public event Action Cleared; 

        public void Add(IngredientType ingredientType)
        {
            if(!_validator.Validate(_ingredients, ingredientType))
                return;
            
            _ingredients.Push(ingredientType);
            
            IngredientAdded?.Invoke(ingredientType);
        }

        public void Clear()
        {
            _ingredients.Clear();
            
            Cleared?.Invoke();
        }
    }
}