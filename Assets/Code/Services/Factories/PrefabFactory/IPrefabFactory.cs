using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Services.Factories.PrefabFactory
{
    public interface IPrefabFactory
    {
        UniTask<T> Create<T>(string assetKey) where T : MonoBehaviour;
        UniTask<T> Create<T>(AssetReference assetReference) where T : MonoBehaviour;
        T Create<T>(T prefab) where T : MonoBehaviour;
    }
}