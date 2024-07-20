using Code.Goods;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create IngredientConfig", fileName = "IngredientConfig", order = 0)]
    public sealed class IngredientConfig : ScriptableObject
    {
        public IngredientType Type;
        public AssetReferenceSprite SpriteReference;
        public AssetReference PrefabReference;
    }
}