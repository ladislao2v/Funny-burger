using Code.Services.AssetProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Code.Services.Factories.PrefabFactory
{
    public class PrefabFactory : IPrefabFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;
        
        public PrefabFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }
        
        public async UniTask<GameObject> Create(string assetKey)
        {
            var prefab = await _assetProvider
                .GetAsset<GameObject>(assetKey);

            return _instantiator.InstantiatePrefab(prefab);
        }

        public async UniTask<GameObject> Create(AssetReference assetReference)
        {
            var prefab = await _assetProvider
                .GetAsset<GameObject>(assetReference);

            return _instantiator.InstantiatePrefab(prefab);
        }

        public T Create<T>(T prefab) where T : MonoBehaviour
        {
            return _instantiator
                .InstantiatePrefabForComponent<T>(prefab);
        }
    }
}