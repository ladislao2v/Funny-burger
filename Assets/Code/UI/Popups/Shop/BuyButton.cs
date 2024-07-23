using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Popups.Shop
{
    public sealed class BuyButton : View
    {
        [SerializeField] private Sprite _unlockSprite;
        [SerializeField] private Sprite _lockSprite;

        private Image _image;
        private Button _button;
        public bool IsActive => _image.sprite == _unlockSprite;
        
        public event Action Clicked;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
        }

        private void OnEnable() => 
            _button.onClick.AddListener(OnClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnClicked);

        public void Enable() => 
            _image.sprite = _unlockSprite;

        public void Disable() => 
            _image.sprite = _lockSprite;

        private void OnClicked() => 
            Clicked?.Invoke();
    }
}