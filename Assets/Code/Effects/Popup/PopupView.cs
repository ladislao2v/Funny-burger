using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Code.Effects.Popup
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private float _duration = 1;
        
        private Tween _tween;
        private Sequence _sequence;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _rectTransform.offsetMin = Vector2.zero;
            _rectTransform.offsetMax = Vector2.zero; 
            _rectTransform.localScale = Vector3.one;
            
            _canvasGroup.alpha = 0;
            
            Show();
        }

        public void Show()
        {
            _tween?.Kill();
            _tween = _canvasGroup.DOFade(1, _duration);
        }
        
        public void Hide(Action callback)
        {
            _sequence = 
                DOTween.Sequence();

            _sequence
                .Append(_canvasGroup.DOFade(0, _duration))
                .AppendCallback(() =>
                {
                    callback?.Invoke();
                });
        }

        private void OnDestroy()
        {
            _tween?.Kill();
            _tween = null;
            _sequence?.Kill();
            _sequence = null;
        }
    }
}
