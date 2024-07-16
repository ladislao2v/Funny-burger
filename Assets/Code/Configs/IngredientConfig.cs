using Code.Goods;
using Code.Ingredients;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create IngredientConfig", fileName = "IngredientConfig", order = 0)]
    public sealed class IngredientConfig : ScriptableObject
    {
        public IngredientType Type;
        public Sprite Sprite;
        public AssetReference AssetReference;
    }
}