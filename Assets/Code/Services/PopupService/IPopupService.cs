using Code.UI.Popups;
using Code.UI.Popups.Reward;
using Cysharp.Threading.Tasks;

namespace Code.Services.PopupService
{
    public interface IPopupService
    {
        UniTask<Popup> ShowPopup(PopupType popupType, RewardData data = null);
    }
}
