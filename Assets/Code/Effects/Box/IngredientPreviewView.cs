using DG.Tweening;
using UnityEngine;

namespace Code.Effects.Box
{
    public class IngredientPreviewView : MonoBehaviour
    {
        private readonly Vector3 _rotationAngle = new Vector3(0, 360, 0);
        
        [SerializeField] private float _moveY;
        [SerializeField] private float _moveDuration;
        [SerializeField] private float _rotationDuration;
        [SerializeField] private Ease _moveEase;
        [SerializeField] private Ease _rotationEase;

        private Vector3 _startPosition;
        private Tween _previewTween;

        private void Awake()
        { 
            _startPosition = transform.localPosition;
        }

        private void OnEnable()
        {
            var moveTween = transform
                .DOLocalMoveY(_startPosition.y + _moveY, _moveDuration)
                .SetEase(_moveEase)
                .SetLoops(-1, LoopType.Yoyo);

            var rotateTween = transform
                .DORotate(_rotationAngle, _rotationDuration, RotateMode.FastBeyond360)
                .SetEase(_rotationEase)
                .SetLoops(-1, LoopType.Restart);

            _previewTween = DOTween.Sequence()
                .Insert(0, moveTween)
                .Insert(0, rotateTween);
        }

        private void OnDisable()
        {
            _previewTween?.Kill();
            _previewTween = null;
        }
    }
}