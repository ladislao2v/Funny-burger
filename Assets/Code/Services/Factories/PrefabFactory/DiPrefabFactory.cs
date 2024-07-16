using Code.Services.AssetProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Code.Services.Factories.PrefabFactory
{
    public class DiPrefabFactory : IPrefabFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;
        
        public DiPrefabFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }
        
        public async UniTask<T> Create<T>(string assetKey) where T : MonoBehaviour
        {
            var prefab = await _assetProvider
                .GetAsset<T>(assetKey); 
            
            return _instantiator
                .InstantiatePrefabForComponent<T>(prefab);
        }

        public async UniTask<T> Create<T>(AssetReference assetReference) where T : MonoBehaviour
        {
            var prefab = await _assetProvider
                .GetAsset<T>(assetReference); 
            
            return _instantiator
                .InstantiatePrefabForComponent<T>(prefab);
        }

        public T Create<T>(T prefab) where T : MonoBehaviour
        {
            return _instantiator
                .InstantiatePrefabForComponent<T>(prefab);
        }
    }
}