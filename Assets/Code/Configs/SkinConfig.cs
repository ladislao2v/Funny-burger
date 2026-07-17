using Code.Skins;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Configs
{
    public abstract class SkinConfig : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
    }

    [CreateAssetMenu(menuName = "Create HatSkinConfig", fileName = "HatSkinConfig", order = 0)]
    public class HatSkinConfig : SkinConfig
    {
        [field: SerializeField] public HatSkinType HatSkinId { get; private set; }
    }
    
    [CreateAssetMenu(menuName = "Create BodySkinConfig", fileName = "BodySkinConfig", order = 0)]
    public class BodySkinConfig : SkinConfig
    {
        [field: SerializeField] public BodySkinType BodySkinId { get; private set; }
        [field: SerializeField] public Material SkinMaterial { get; private set; }
    }
}