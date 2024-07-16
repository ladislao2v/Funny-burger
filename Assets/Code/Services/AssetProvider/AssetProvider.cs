using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Code.Services.AssetProvider
{
    public class AssetProvider : IAssetProvider
    {
        private Dictionary<string, AsyncOperationHandle> _completed = new();
        private Dictionary<string, List<AsyncOperationHandle>> _handles = new();

        public bool IsLoaded { get; private set; } = false;

        public async UniTask Load()
        {
            await Addressables.InitializeAsync();

            IsLoaded = true;
        }
        public async UniTask<T> GetAsset<T>(AssetReference assetReference) where T : class
        {
            if (_completed.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);
            string assetKey = assetReference.AssetGUID;
            
            return await SubscribeCompletedHandle(handle, assetKey);
        }

        public async UniTask<T> GetAsset<T>(string assetKey) where T : class
        {
            if (_completed.TryGetValue(assetKey, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetKey);

            return await SubscribeCompletedHandle(handle, assetKey);
        }

        public void Clean()
        {
            foreach (List<AsyncOperationHandle> resourceHandles  in _handles.Values)
                resourceHandles.ForEach(x => Addressables.Release(x));
        }

        private async UniTask<T> SubscribeCompletedHandle<T>(AsyncOperationHandle<T> handle, string assetKey) where T : class
        {
            handle.Completed += h => 
                _completed.Add(assetKey, h);

            AddHandle(assetKey, handle);

            return await handle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles.Add(key, resourceHandles);
            }

            resourceHandles.Add(handle);
        }
    }
}