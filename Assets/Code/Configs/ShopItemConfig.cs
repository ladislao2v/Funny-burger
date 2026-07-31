using Code.Services.ResourceStorage;
using Code.Services.ShopService;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create ShopItem", fileName = "ShopItem", order = 0)]
    public class ShopItemConfig : ScriptableObject
    {
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public ResourceType Currency { get; private set; }
        [field: SerializeField] public int RequiredLevel { get; private set; }
        [field: SerializeField] public Item Item { get; private set; }
        [field: SerializeField] public ShopType ShopType { get; private set; }
    }
}