namespace Code.Services.PopupService
{
    public sealed class PopupContainerProvider : IPopupContainerProvider
    {
        public IPopupContainer Container { get; private set; }

        public void Set(IPopupContainer popupContainer) => 
            Container = popupContainer;
    }
}