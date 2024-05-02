using System;

namespace Code.Services.SceneLoader
{
    public interface ISceneLoader
    {
        void LoadScene(string name, Action onLoaded = null);
        void RestartScene(Action onLoaded = null);
    }
}