using Code.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Order
{
    public class OrderView : View, IOrderView
    {
        [SerializeField] private Image _orderLogo;
        [SerializeField] private TextMeshProUGUI _cost;
        
        public void OnOrder(RecipeConfig config)
        {
            _orderLogo.sprite = config.OrderLogo;
            _cost.text = $"COST: {config.Price}";
        }
    }
}