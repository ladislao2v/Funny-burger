using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Code.Effects.Popup
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private Vector3 _scale = Vector3.one;
        [SerializeField] private float _duration = 1;
        
        private TweenerCore<Vector3,Vector3,VectorOptions> _tween;
        private Sequence _sequence;
        
        private void Start()
        {
            ScaleUp();
        }

        public void ScaleUp()
        {
            _tween?.Kill();
            _tween = transform.DOScale(_scale, _duration);
        }
        
        public void ScaleDown(Action callback)
        {
            _sequence = 
                DOTween.Sequence();

            _sequence
                .Append(transform.DOScale(Vector3.zero, _duration))
                .AppendCallback(() => callback?.Invoke());
        }

        private void OnDestroy()
        {
            _tween.Kill();
            _sequence.Kill();
        }
    }
}
