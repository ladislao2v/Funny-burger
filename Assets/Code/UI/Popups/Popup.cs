using System;
using Code.Services.PopupService;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Popups
{
    public abstract class Popup : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        
        public IPopupData Data { get; private set; }

        public void Construct(IPopupData data)
        {
            Data = data;
        }

        public event Action Clicked;

        private void Start() => 
            _closeButton.onClick.AddListener(OnClicked);

        private void OnDestroy() => 
            _closeButton.onClick.RemoveListener(OnClicked);

        public void Close() => Destroy(gameObject);

        private void OnClicked() => 
            Clicked?.Invoke();
    }
}