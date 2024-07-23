using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Services.ShopService;
using UnityEngine;
using static Code.Goods.IngredientType;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create Recipe", fileName = "Recipe", order = 0)]
    public sealed class RecipeConfig : ScriptableObject, IShopItem
    {
        private readonly int _maxIngredientsCount = 6;
        
        [Header("Main")]
        [SerializeField] private Sprite _logo;
        [SerializeField] private string _description;
        [SerializeField] private int _price;
<<<<<<< HEAD:Assets/Code/Configs/RecipeConfig.cs
        [SerializeField] private int _requiredLevel;
        
        [Header("Data")]
        [SerializeField] private List<IngredientConfig> _burger;

        public Sprite Logo => _logo;
        public string Description => _description;
=======
        public IEnumerable<IngredientConfig> Burger => _burger;
>>>>>>> 3f2981a6ce365d14b31d0e298b0e3d3d8fd23979:Assets/Code/Recipes/Recipe.cs
        public int Price => _price;
        public int RequiredLevel => _requiredLevel;
        public IEnumerable<IngredientConfig> Burger => _burger;

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