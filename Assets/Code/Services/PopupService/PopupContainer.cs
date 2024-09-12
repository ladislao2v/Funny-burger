using System;
using Code.Effects.Popup;
using Code.UI.Popups;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Services.PopupService
{
    public class PopupContainer : MonoBehaviour, IPopupContainer
    {
        [SerializeField] private Vector3 _popupPosition;
        [SerializeField] private Button _background;

        private Popup _current;
        private IDisposable _timer;

        public void Put(Popup popup)
        {
            if(_current != null)
                Hide();
            
            _background.onClick.AddListener(Hide);
            _background.gameObject.SetActive(true);

            _current = popup;

            var popupTransform = popup.transform;
            
            popupTransform.SetParent(transform);
            popupTransform.localPosition = _popupPosition;

            _timer = Observable
                .Timer(TimeSpan.FromSeconds(0.175f))
                .Subscribe(_ =>
                {
                    Time.timeScale = 0f;
                    _timer.Dispose();
            });

            popup.Clicked += Hide;
        }

        private void Hide()
        {
            CloseLastPopup();
            
            _background.gameObject.SetActive(false);
            _background.onClick.RemoveAllListeners();

            Time.timeScale = 1f;
        }

        private void CloseLastPopup()
        {
            _current.Clicked -= Hide;
            _current
                .GetComponent<PopupView>()
                .ScaleDown(_current.Close);
            _current = null;
        }
    }
}