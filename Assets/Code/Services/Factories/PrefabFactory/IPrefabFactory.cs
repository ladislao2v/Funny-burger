using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Services.Factories.PrefabFactory
{
    public interface IPrefabFactory
    {
        UniTask<GameObject> Create(string assetKey);
        UniTask<GameObject> Create(AssetReference assetReference);
        T Create<T>(T prefab) where T : MonoBehaviour;
    }
}