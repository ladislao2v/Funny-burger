using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Code.Services.SceneLoader
{
    public sealed class SceneLoader : ISceneLoader
    {
        private SceneInstance _sceneInstance;

        public async void LoadScene(string name, Action onLoaded = null)
        {
            AsyncOperationHandle<SceneInstance> handle =  Addressables.LoadSceneAsync(name);

            handle.Completed += (_) => onLoaded?.Invoke();

            _sceneInstance = await handle.ToUniTask();
        }

        public async void RestartScene(Action onLoaded = null)
        {
            await SceneManager
                .LoadSceneAsync(_sceneInstance.Scene.name)
                .ToUniTask();
        }
    }
}