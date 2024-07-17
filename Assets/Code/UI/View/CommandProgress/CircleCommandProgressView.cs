using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.View.CommandProgress
{
    public class CircleCommandProgressView : View, ICommandProgressView
    {
        [SerializeField] private Image _bar;
        
        private float _minValue = 0;
        private float _maxValue = 1;

        public void OnCommandProgressed(float duration)
        {
            _bar.fillAmount = 0;
            _bar.DOFillAmount(_maxValue, duration);
        }
    }
}