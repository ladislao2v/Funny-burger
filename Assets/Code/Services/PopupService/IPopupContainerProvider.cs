namespace Code.Services.PopupService
{
    public interface IPopupContainerProvider
    {
        IPopupContainer Container { get; }

        void Set(IPopupContainer popupContainer);
    }
}