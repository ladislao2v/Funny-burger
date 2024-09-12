using System;
using Code.Services.ShopService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Shop
{
    public sealed class ShopItemView : View, IShopItemView
    {
        [SerializeField] private Image _logo;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private BuyButton _button;
        
        private IShopItem _shopItem;
        
        public IShopItem ShopItem => _shopItem;
        public bool IsActive => _button.IsActive;

        public event Action<IShopItem> BuyButtonClicked;
        
        public void Construct(IShopItem shopItem)
        {
            _shopItem = shopItem;

            _logo.sprite = _shopItem.Logo;
            _name.text = _shopItem.Name;
            _description.text = string.Format(_shopItem.Description, "\n");
            _level.text = string.Format(_level.text, _shopItem.RequiredLevel);

            _button.Construct(shopItem.Price);
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
            transform.localScale = Vector3.one;
        }

        private void OnEnable() => 
            _button.Clicked += OnBuyButtonClicked;
        private void OnDisable() => 
            _button.Clicked -= OnBuyButtonClicked;

        public void EnableButton() => 
            _button.Enable();
        public void DisableButton(bool isBought) => 
            _button.Disable(isBought);

        private void OnBuyButtonClicked() => 
            BuyButtonClicked?.Invoke(_shopItem);
    }
}