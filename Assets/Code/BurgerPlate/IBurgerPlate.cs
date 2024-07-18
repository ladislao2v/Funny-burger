using System;
using Code.Goods;

namespace Code.BurgerPlate
{
    public interface IBurgerPlate
    {
        public bool IsEmpty { get; }

        event Action<IngredientType> IngredientAdded;  
        event Action Cleared;  

        void Add(IngredientType ingredientType);
        bool Contains(IngredientType topBun);
        void Clear();
    }
}