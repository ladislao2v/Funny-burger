using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Extensions;
using UnityEngine;
using static Code.Goods.IngredientType;

namespace Code.Recipes
{
    [CreateAssetMenu(menuName = "Create Recipe", fileName = "Recipe", order = 0)]
    public class Recipe : ScriptableObject
    {
        private readonly int _maxIngredientsCount = 6;
        
        [SerializeField] private List<IngredientConfig> _burger;

        public IReadOnlyList<IngredientConfig> Burger => _burger;

        private void OnValidate()
        {
            var first = _burger.First();
            var preLast = _burger.PreLast();
            var last = _burger.Last();
            
            if (_burger.Count == 1 && first.Type != BottomBun)
                _burger.Clear();

            if (preLast.Type == TopBun)
                _burger.Remove(last);

            if (_burger.Count == _maxIngredientsCount && last.Type != TopBun)
                _burger.Remove(last);
        }
    }
}