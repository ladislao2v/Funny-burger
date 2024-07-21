using UnityEngine;

namespace Code.Services.PopupService
{
    public interface IPopupService
    {
        void ShowPopup(PopupType popupType);
    }
}