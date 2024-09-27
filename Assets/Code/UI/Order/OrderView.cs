using Code.Configs;
using Code.Constants;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Order
{
    public class OrderView : View, IOrderView
    {
        [SerializeField] private Image _orderLogo;
        [SerializeField] private Image _progress;
        
        private Tween _progressFilling;

        private void OnDisable() => _progressFilling.Kill();

        public void OnOrder(RecipeConfig config)
        {
            if(config == null)
                return;
            
            Show();
            
            _orderLogo.sprite = config.OrderLogo;

            StartTimerView(config.CookTime);
        }

        private void StartTimerView(float duration)
        {
            _progress.fillAmount = ProgressBar.Max;
            _progressFilling = _progress
                .DOFillAmount(ProgressBar.Min, duration);
        }
    }
}