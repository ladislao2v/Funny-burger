using Code.Goods;
using Code.Ingredients;
using UnityEngine;

namespace Code.Services.StaticDataService
{
    [CreateAssetMenu(menuName = "Create IngredientConfig", fileName = "IngredientConfig", order = 0)]
    public class IngredientConfig : ScriptableObject
    {
        public IngredientType Type;
        public Ingredient Prefab;
    }
}