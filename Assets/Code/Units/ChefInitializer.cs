using System.Runtime.InteropServices;
using Code.CompositionRoot;
using Code.Configs;
using Code.Services.ConfigProvider;
using UnityEngine;
using Zenject;

namespace Code.Units
{
    public sealed class ChefInitializer : MonoBehaviour
    {
        private IConfigProvider _configProvider;

        [Inject]
        private void Construct(IConfigProvider configProvider) => 
            _configProvider = configProvider;

        public void Awake()
        {
            IChefConfig config = 
                _configProvider.SettingsConfig;
            
            GetComponent<IPlayer>()
                .Construct(config);
        }
    }
}