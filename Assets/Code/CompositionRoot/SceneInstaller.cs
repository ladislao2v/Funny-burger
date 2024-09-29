using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.CompositionRoot
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private List<MonoBehaviour> _initialables;

        public override void InstallBindings()
        {
            foreach (MonoBehaviour initialable in _initialables)
            {
                Container.BindInterfacesTo(initialable.GetType()).FromInstance(initialable).AsSingle();
            }
        }
    }
}