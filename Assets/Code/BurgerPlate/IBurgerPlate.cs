using System;
using System.Collections.Generic;
using Code.Goods;

namespace Code.BurgerPlate
{
    public interface IBurgerPlate
    {
        public IReadOnlyCollection<IngredientType> Ingredients { get; }
        public bool IsEmpty { get; }

        event Action<IngredientType> IngredientAdded;
        event Action<IngredientType[]> IngredientsAdded;
        event Action Cleared;  

        void Add(IngredientType ingredientType);
        void AddRange(IngredientType[] ingredients);
        bool Contains(IngredientType topBun);
        void Clear();
    }
}