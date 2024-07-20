using Code.UI.Popups;
using UnityEngine;

namespace Code.Services.PopupService
{
    public class PopupContainer : MonoBehaviour, IPopupContainer
    {
        [SerializeField] private Vector3 _popupPosition;

        private Popup _current;
        
        public void LinkPopup(Popup popup)
        {
            if(_current != null)
                CloseCurrentPopup();

            _current = popup;

            var popupTransform = popup.transform;
            
            popupTransform.SetParent(transform);
            popupTransform.localPosition = _popupPosition;
        }

        private void CloseCurrentPopup()
        {
            _current.Close();
            _current = null;
        }
    }
}