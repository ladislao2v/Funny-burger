using System;
using Code.Services.ShopService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Popups.Shop
{
    public sealed class ShopItemView : View, IShopItemView
    {
        [SerializeField] private Image _logo;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private BuyButton _button;
        
        private IShopItem _shopItem;
        
        public IShopItem ShopItem => _shopItem;
        public bool IsActive => _button.IsActive;
        

        public event Action<IShopItem> BuyButtonClicked;
        

        public void Construct(IShopItem recipeConfig)
        {
            _shopItem = recipeConfig;

            _logo.sprite = _shopItem.Logo;
            _description.text = _shopItem.Description;
        }

        public void SetParent(Transform parent) => 
            transform.SetParent(parent);

        private void OnEnable() => 
            _button.Clicked += OnBuyButtonClicked;
        private void OnDisable() => 
            _button.Clicked -= OnBuyButtonClicked;

        public void EnableButton() => 
            _button.Enable();
        public void DisableButton() => 
            _button.Disable();

        private void OnBuyButtonClicked() => 
            BuyButtonClicked?.Invoke(_shopItem);
    }
}