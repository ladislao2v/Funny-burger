using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Shop
{
    public sealed class BuyButton : View
    {
        [SerializeField] private TextMeshProUGUI _priceView;
        [SerializeField] private Image _coin;
        [SerializeField] private Image _lock;
        [SerializeField] private Image _mark;
        
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
            _mark.enabled = false;
            _button.interactable = true;
        }

        public void Disable(bool isBought = false)
        {
            if (isBought)
                SetBoughtState();
            else
                _lock.enabled = true;
                
            _button.interactable = false;
        }

        private void SetBoughtState()
        {
            _mark.enabled = true;
            _priceView.enabled = false;
            _coin.enabled = false;
        }

        private void OnClicked() => 
            Clicked?.Invoke();
    }
}