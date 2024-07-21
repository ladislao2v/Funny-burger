using System;
using System.Collections.Generic;
using Code.Goods;

namespace Code.BurgerPlate
{
    public interface IBurgerPlate
    {
        public IEnumerable<IngredientType> Ingredients { get; }
        public bool IsEmpty { get; }

        event Action<IngredientType> IngredientAdded;
        event Action<IEnumerable<IngredientType>> IngredientsAdded;
        event Action Cleared;  

        void Add(IngredientType ingredientType);
        void AddRange(IEnumerable<IngredientType> ingredients);
        bool Contains(IngredientType topBun);
        void Clear();
    }
}