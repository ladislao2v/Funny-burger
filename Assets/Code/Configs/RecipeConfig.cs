using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Services.ShopService;
using UnityEngine;
using static Code.Ingredients.IngredientType;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create Recipe", fileName = "Recipe", order = 0)]
    public sealed class RecipeConfig : ScriptableObject, IItem
    {
        private readonly int _maxIngredientsCount = 6;
        
        [Header("Main")]
        [SerializeField] private Sprite _logo;
        [SerializeField] private Sprite _orderLogo;
        [SerializeField] private string _name;
        [SerializeField, Min(0)] private int _price;
        [SerializeField, Min(0)] private int _requiredLevel;
        [SerializeField, Range(0, 90)] private float _cookTime;
        [SerializeField] private bool _isStart;


        [Header("Data")]
        [SerializeField] private List<IngredientConfig> _burger;

        public Sprite Logo => _logo;
        public Sprite OrderLogo => _orderLogo;
        public string Name => _name;
        public int Price => _price;
        public int RequiredLevel => _requiredLevel;
        public float CookTime => _cookTime;
        public bool IsStart => _isStart;
        public IEnumerable<IngredientConfig> Burger => _burger;

        private void OnValidate()
        {
            _name = name;
            
            if (_burger.Count == 1 && _burger.First()?.Type != TopBun)
                _burger.Clear();

            if (_burger.Count >= 2 && _burger.PreLast()?.Type == BottomBun)
                _burger.Remove( _burger.Last());

            if (_burger.Count == _maxIngredientsCount &&  _burger.Last()?.Type != BottomBun)
                _burger.Remove( _burger.Last());
        }
    }
}