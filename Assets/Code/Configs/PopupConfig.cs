using Code.Services.PopupService;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Create PopupConfig", fileName = "PopupConfig", order = 0)]
    public class PopupConfig : ScriptableObject
    {
        [field: SerializeField ]public PopupType Type { get; private set; }
        [field: SerializeField] public AssetReference PrefabReference { get; private set; }
    }
}