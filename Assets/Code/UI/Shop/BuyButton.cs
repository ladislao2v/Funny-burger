using System;
using Code.Services.ResourceStorage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Shop
{
    public sealed class BuyButton : View
    {
        [SerializeField] private TextMeshProUGUI _priceView;
        [SerializeField] private GameObject _gems;
        [SerializeField] private GameObject _coins;
        
        private Button _button;

        public event Action Clicked;

        public void Construct(int price, ResourceType currency)
        {
            _priceView.text = price.ToString();
            
            _gems.SetActive(false);
            _coins.SetActive(false);
            
            if(currency == ResourceType.Coin)
                _coins.SetActive(true);
            else
                _gems.SetActive(true);
        }

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