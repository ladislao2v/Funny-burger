using System;
using Code.Configs;
using Code.Services.ResourceStorage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Shop
{
    public sealed class ItemView : View, IItemView
    {
        [SerializeField] private Image _logo;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private BuyButton _button;
        //[SerializeField] private GameObject _dark;
        [SerializeField] private GameObject _lock;
        
        private Item _item;
        
        public Item Item => _item;

        public event Action<Item> BuyButtonClicked;
        public event Action<Item> SelectButtonClicked;

        public void Construct(Item item, int? level = null, ResourceType? currency = null, int? price = null)
        {
            _item = item;

            _logo.sprite = _item.Logo;
            _name.text = _item.Name;
            
            if (level.HasValue)
                _level.text = string.Format(_level.text, level.Value);

            if (price.HasValue && currency.HasValue)
                _button.Construct(price.Value, currency.Value);
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
            
        public void ChangeItemState(ItemState state)
        {
            if (state == ItemState.CanBuy)
                SetCanBuyState();
            else if (state == ItemState.Select)
                SetSelectState();
            else if (state == ItemState.Selected)
                SetSelectedState();
            else if (state == ItemState.Money)
                SetNotMoneyState();
            else if (state == ItemState.Level) 
                SetLockedState();
        }

        private void SetCanBuyState()
        {
            _lock.SetActive(false);
            //_dark.SetActive(false);
            _button.ChangePriceColor(true);
            _button.Enable();
        }

        private void SetLockedState()
        {
            _button.Disable();
            _lock.SetActive(true);
        }

        private void SetNotMoneyState()
        {
            _lock.SetActive(false);
            //_dark.SetActive(true);
            _button.ChangePriceColor(false);
            _button.Enable();
        }

        private void SetSelectedState()
        {
            _lock.SetActive(false);
            //_dark.SetActive(false);
            _button.SetSelectedState();
        }
        
        private void SetSelectState()
        {
            _lock.SetActive(false);
            //_dark.SetActive(false);
            _button.SetUnselectedState();
        }

        private void OnBuyButtonClicked() => 
            BuyButtonClicked?.Invoke(_item);
    }
}