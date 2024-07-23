using Code.Configs;
using Code.Services.AudioService;
using Code.Services.ConfigProvider;
using Code.Services.Factories.PrefabFactory;
using Code.Services.PopupService;
using Code.UI.Popups;
using Code.UI.Popups.Settings;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Factories.PopupFactory
{
    public class PopupFactory : IPopupFactory
    {
        private readonly IPrefabFactory _prefabFactory;
        private readonly IConfigProvider _configProvider;
        private readonly IAudioService _audioService;

        public PopupFactory(IPrefabFactory prefabFactory, IConfigProvider configProvider)
        {
            _prefabFactory = prefabFactory;
            _configProvider = configProvider;
        }
        
        public async UniTask<Popup> Create(PopupType popupType)
        {
            PopupConfig config = _configProvider
                .GetPopupConfig(popupType);
            
            GameObject popup = await _prefabFactory
                .Create(config.PrefabReference);

            return popup.GetComponent<Popup>();
        }
    }
}