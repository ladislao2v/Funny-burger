using System.Collections.Generic;
using System.Linq;
using Code.Extensions;
using Code.Services.LevelRewardService;
using Code.Services.ShopService;
using UnityEngine;
using static Code.Ingredients.IngredientType;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create Recipe", fileName = "Recipe", order = 0)]
    public sealed class RecipeConfig : Item
    {
        private readonly int _maxIngredientsCount = 6;
        
        [Header("Main")]
        [field: SerializeField] public Sprite OrderLogo { get; private set;}
        [field: SerializeField, Range(0, 90)] public float CookTime { get; private set; }
        [field: SerializeField] public bool IsStart { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        
        [Header("Data")]
        [SerializeField] private List<IngredientConfig> _burger;

        public IEnumerable<IngredientConfig> Burger => _burger;

        private void OnValidate()
        {
            if (_burger.Count == 1 && _burger.First()?.Type != TopBun)
                _burger.Clear();

            if (_burger.Count >= 2 && _burger.PreLast()?.Type == BottomBun)
                _burger.Remove( _burger.Last());

            if (_burger.Count == _maxIngredientsCount &&  _burger.Last()?.Type != BottomBun)
                _burger.Remove( _burger.Last());
        }

        public override void Accept(IItemVisitor itemVisitor) => 
            itemVisitor.Visit(this);
    }
}