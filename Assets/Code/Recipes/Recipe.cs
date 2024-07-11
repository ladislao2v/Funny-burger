using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Goods;
using Code.Services.StaticDataService;
using UnityEngine;

namespace Code.Recipes
{
    [CreateAssetMenu(menuName = "Create Recipe", fileName = "Recipe", order = 0)]
    public class Recipe : ScriptableObject
    {
        [SerializeField] private List<IngredientConfig> _burger;

        public IReadOnlyList<IngredientConfig> Burger => _burger;

        private void OnValidate()
        {
            if (_burger.Count == 1 && _burger[0].Type != IngredientType.BottomBun)
                _burger.Clear();

            if (_burger.PreLast().Type == IngredientType.TopBun)
                _burger.Remove(_burger.Last());
        }
    }
}