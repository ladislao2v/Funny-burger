using Code.Services.AssetProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Services.Factories.PrefabFactory
{
    public class PrefabFactory<T> : IPrefabFactory<T> where T : MonoBehaviour
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public PrefabFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public async UniTask<T> Create()
        {
            var prefab = await _assetProvider
                .GetPrefab<T>(ResourcesPaths.PrefabsPath); 
            
            return _instantiator
                .InstantiatePrefabForComponent<T>(prefab);
        }
    }
}