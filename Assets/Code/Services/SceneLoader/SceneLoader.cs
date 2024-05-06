using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Code.Services.SceneLoader
{
    public sealed class SceneLoader : ISceneLoader
    {
        public async void LoadScene(string name, Action onLoaded = null)
        {
            await SceneManager.LoadSceneAsync(name);
            
            onLoaded?.Invoke();
        }

        public async void RestartScene(Action onLoaded = null)
        {
            Scene scene = SceneManager.GetActiveScene();
            
            LoadScene(scene.name, onLoaded);
        }
    }
}