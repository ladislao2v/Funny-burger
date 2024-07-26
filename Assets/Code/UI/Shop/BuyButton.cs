using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Popups.Shop
{
    public sealed class BuyButton : View
    {
        [SerializeField] private TextMeshProUGUI _priceView;
        [SerializeField] private Image _lock;
        
        private Button _button;
        public bool IsActive => _lock.enabled;
        
        public event Action Clicked;

        public void Construct(int price) => 
            _priceView.text = price.ToString();

        private void Awake() => 
            _button = GetComponent<Button>();

        private void OnEnable() => 
            _button.onClick.AddListener(OnClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnClicked);

        public void Enable()
        {
            _lock.enabled = false;
            _button.interactable = true;
        }

        public void Disable()
        {
            _lock.enabled = true;
            _button.interactable = false;
        }

        private void OnClicked() => 
            Clicked?.Invoke();
    }
}