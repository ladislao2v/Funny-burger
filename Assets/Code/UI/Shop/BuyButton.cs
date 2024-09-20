using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Shop
{
    public sealed class BuyButton : View
    {
        [SerializeField] private TextMeshProUGUI _priceView;
        
        private Button _button;

        public event Action Clicked;

        public void Construct(int price) => 
            _priceView.text = price.ToString();

        private void Awake() => 
            _button = GetComponent<Button>();

        private void OnEnable() => 
            _button.onClick.AddListener(OnClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnClicked);

        public void Enable() => 
            gameObject.SetActive(true);

        public void Disable() => 
            gameObject.SetActive(false);

        private void OnClicked() => 
            Clicked?.Invoke();
    }
}