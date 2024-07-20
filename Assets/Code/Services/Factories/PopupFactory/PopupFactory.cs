using Code.Configs;
using Code.Services.Factories.PrefabFactory;
using Code.Services.PopupService;
using Code.Services.StaticDataService;
using Code.UI.Popups;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Factories.PopupFactory
{
    public class PopupFactory : IPopupFactory
    {
        private readonly IPrefabFactory _prefabFactory;
        private readonly IStaticDataService _staticDataService;

        public PopupFactory(IPrefabFactory prefabFactory, IStaticDataService staticDataService)
        {
            _prefabFactory = prefabFactory;
            _staticDataService = staticDataService;
        }
        
        public async UniTask<Popup> Create(PopupType popupType)
        {
            PopupConfig config = _staticDataService
                .GetPopupConfig(popupType);
            
            GameObject popup = await _prefabFactory
                .Create(config.PrefabReference);

            return popup.GetComponent<Popup>();
        }
    }
}