using Code.Services.Factories.PopupFactory;
using Code.UI.Popups;

namespace Code.Services.PopupService
{
    public sealed class PopupService : IPopupService
    {
        private readonly IPopupFactory _popupFactory;
        private readonly IPopupContainerProvider _popupContainerProvider;

        public PopupService(IPopupFactory popupFactory, 
            IPopupContainerProvider popupContainerProvider)
        {
            _popupFactory = popupFactory;
            _popupContainerProvider = popupContainerProvider;
        }
        
        public async void ShowPopup(PopupType popupType, IPopupData data = null)
        {
            Popup popup = 
                await _popupFactory.Create(popupType);
            
            popup.Construct(data);
            
            _popupContainerProvider.Container.Put(popup);
        }
    }
}