using Code.UI.Popups;

namespace Code.Services.PopupService
{
    public interface IPopupContainer
    {
        bool HasPopup { get; }

        void Put(Popup popup);
    }
}
