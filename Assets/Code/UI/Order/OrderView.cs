using Code.Configs;
using Code.Constants;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Order
{
    public class OrderView : View, IOrderView
    {
        [SerializeField] private Image _orderLogo;
        [SerializeField] private Image _progress;
        [SerializeField] private TMP_Text _timer;
        
        private Tween _progressFilling;
        private Tween _progressCounting;

        private void OnDisable() =>
            StopTimerView();

        public void OnOrder(RecipeConfig config, bool withTimer, float cookTime)
        {
            if(config == null)
                return;

            Show();

            _orderLogo.sprite = config.OrderLogo;

            StopTimerView();
            _timer.gameObject.SetActive(withTimer);

            if (withTimer)
                StartTimerView(cookTime);
            else
                _progress.fillAmount = ProgressBar.Max;
        }

        private void StopTimerView()
        {
            _progressFilling?.Kill();
            _progressCounting?.Kill();
        }

        private void StartTimerView(float duration)
        {
            _progress.fillAmount = ProgressBar.Max;
            _progressFilling = _progress
                .DOFillAmount(ProgressBar.Min, duration);
            _progressCounting = _timer
                .DOCounter((int)duration, ProgressBar.Min, duration);
        }
    }
}