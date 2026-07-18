using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Configs
{
    public abstract class SkinConfig : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
    }
}