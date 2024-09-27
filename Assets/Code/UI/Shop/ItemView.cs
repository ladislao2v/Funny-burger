using System;
using Code.Services.ShopService;
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
        [SerializeField] private GameObject _dark;
        [SerializeField] private GameObject _mark;
        [SerializeField] private GameObject _lock;
        
        private IItem _item;
        
        public IItem Item => _item;

        public event Action<IItem> BuyButtonClicked;
        public event Action<IItem> SelectButtonClicked;

        public void Construct(IItem item)
        {
            _item = item;

            _logo.sprite = _item.Logo;
            _name.text = _item.Name;
            _level.text = string.Format(_level.text, _item.RequiredLevel);

            _button.Construct(item.Price);
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
            switch (state)
            {
                case ItemState.CanBuy:
                    SetCanBuyState();
                    break;
                case ItemState.Selected:
                    SetBoughtState();
                    break;
                case ItemState.Money:
                    SetNotMoneyState();
                    break;
                case ItemState.Level:
                    SetLockedState();
                    break;
            }
        }

        private void SetCanBuyState()
        {
            _mark.SetActive(false);
            _lock.SetActive(false);
            _dark.SetActive(false);
            _button.Enable();
        }

        private void SetLockedState()
        {
            _button.Disable();
            _mark.SetActive(false);
            _lock.SetActive(true);
        }

        private void SetNotMoneyState()
        {
            _mark.SetActive(false);
            _lock.SetActive(false);
            _dark.SetActive(true);
            _button.Enable();
        }

        private void SetBoughtState()
        {
            _lock.SetActive(false);
            _dark.SetActive(false);
            _button.Disable();
            _mark.SetActive(true);
        }

        private void OnBuyButtonClicked() => 
            BuyButtonClicked?.Invoke(_item);
    }
}