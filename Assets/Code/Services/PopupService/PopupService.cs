using Code.Services.Factories.PopupFactory;
using Code.UI.Popups;

namespace Code.Services.PopupService
{
    public sealed class PopupService : IPopupService
    {
        private readonly IPopupFactory _popupFactory;
        private readonly IPopupContainer _popupContainer;

        public PopupService(IPopupFactory popupFactory, 
            IPopupContainerProvider popupContainerProvider)
        {
            _popupFactory = popupFactory;
            _popupContainer = popupContainerProvider.Container;
        }
        
        public async void ShowPopup(PopupType popupType)
        {
            Popup popup = 
                await _popupFactory.Create(popupType);
            
            _popupContainer.Put(popup);
        }
    }
}