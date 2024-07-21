using Code.Configs;
using Code.Services.AudioService;
using Code.Services.Factories.PrefabFactory;
using Code.Services.PopupService;
using Code.Services.StaticDataService;
using Code.UI.Popups;
using Code.UI.Popups.Settings;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Factories.PopupFactory
{
    public class PopupFactory : IPopupFactory
    {
        private readonly IPrefabFactory _prefabFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IAudioService _audioService;

        public PopupFactory(IPrefabFactory prefabFactory, IStaticDataService staticDataService, 
            IAudioService audioService)
        {
            _prefabFactory = prefabFactory;
            _staticDataService = staticDataService;
            _audioService = audioService;
        }
        
        public async UniTask<Popup> Create(PopupType popupType)
        {
            PopupConfig config = _staticDataService
                .GetPopupConfig(popupType);
            
            GameObject popupGameObject = await _prefabFactory
                .Create(config.PrefabReference);

            Popup popup = popupGameObject
                .GetComponent<Popup>();

            if (popup is SettingsPopup settingsPopup)
                settingsPopup.Construct(_audioService);
            
            return popup;
        }
    }
}