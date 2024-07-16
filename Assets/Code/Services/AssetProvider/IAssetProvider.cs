using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Services.AssetProvider
{
    public interface IAssetProvider
    {
        bool IsLoaded { get; }
        
        UniTask Load();
        UniTask<T> GetAsset<T>(AssetReference assetReference) where T : class;
        UniTask<T> GetAsset<T>(string assetKey) where T : class;
        void Clean();
    }
}