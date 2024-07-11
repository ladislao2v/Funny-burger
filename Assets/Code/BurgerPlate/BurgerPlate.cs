using System.Collections.Generic;
using Code.Ingredients;
using ModestTree;
using UnityEngine;

namespace Code.BurgerPlate
{
    public class BurgerPlate : MonoBehaviour, IBurgerPlate
    {
        private readonly Stack<Ingredient> _ingredients = new();

        [SerializeField] private Transform _firstIngredientPoint;
        [SerializeField] private Vector3 _offsetBetweenIngredients = new Vector3(0, 1, 0);

        public bool IsEmpty => _ingredients.IsEmpty();

        public void Add(Ingredient ingredient)
        {
            if (_ingredients.IsEmpty() && ingredient is not BottomBun)
                return;

            if (ingredient is BottomBun && _ingredients.Contains(ingredient))
                return;

            if (ingredient is TopBun && _ingredients.Contains(ingredient))
                return;

            ingredient.transform.position = GetIngredientPlacementPosition();

            _ingredients.Push(ingredient);
        }

        public void Clear()
        {
            foreach (Ingredient ingredient in _ingredients)
                Destroy(ingredient);
            
            _ingredients.Clear();
        }

        private Vector3 GetIngredientPlacementPosition()
        {
            Vector3 lastPosition;

            if (_ingredients.TryPeek(out Ingredient last))
                lastPosition = last.transform.position;
            else
                lastPosition = _firstIngredientPoint.position;

            var newPosition = lastPosition + _offsetBetweenIngredients;
            
            return newPosition;
        }
    }
}