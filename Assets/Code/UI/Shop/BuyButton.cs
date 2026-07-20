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
        [SerializeField] private SelectionData _selectionData;
        
        private Button _button;
        private Image _image;

        public event Action Clicked;

        public void Construct(int price, ResourceType currency)
        {
            SetBuyState(price, currency);
        }

        private void SetBuyState(int price, ResourceType currency)
        {
            _priceView.text = price.ToString();
            
            _gems.SetActive(false);
            _coins.SetActive(false);
            
            if(currency == ResourceType.Coin)
                _coins.SetActive(true);
            else
                _gems.SetActive(true);
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = _button.GetComponent<Image>();
        }

        private void OnEnable() => 
            _button.onClick.AddListener(OnClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnClicked);

        public void Enable() => 
            gameObject.SetActive(true);

        public void Disable() => 
            gameObject.SetActive(false);

        public void ChangePriceColor(bool canBuy) => _priceView.color = canBuy 
            ? Color.white 
            : Color.red;

        public void SetSelectedState()
        {
            _gems.SetActive(false);
            _coins.SetActive(false);
            _priceView.enabled = false;
            
            _image.sprite = _selectionData.Background;
            _selectionData.SelectedText.enabled = true;
            _selectionData.UnselectedText.enabled = false;
        }

        public void SetUnselectedState()
        {
            _gems.SetActive(false);
            _coins.SetActive(false);
            _priceView.enabled = false;
            
            _image.sprite = _selectionData.Background;
            _selectionData.SelectedText.enabled = false;
            _selectionData.UnselectedText.enabled = true;
        }

        private void OnClicked() => 
            Clicked?.Invoke();
    }

    [Serializable]
    public class SelectionData
    {
        public Sprite Background;
        public TextMeshProUGUI SelectedText;
        public TextMeshProUGUI UnselectedText;
    }
}