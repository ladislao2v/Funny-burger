using Code.UI.Popups.Reward;

namespace Code.Services.PopupService
{
    public interface IPopupService
    {
        void ShowPopup(PopupType popupType, RewardData data = null);
    }
}