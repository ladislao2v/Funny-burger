using DG.Tweening;
using UnityEngine;

namespace Code.Effects.Box
{
    public class TutorialArrowView : MonoBehaviour
    {
        [SerializeField] private GameObject _arrow;
        [SerializeField] private float _moveY;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        private Tween _arrowTween;
        private Vector3 _startPosition;

        private void Awake() =>
            _startPosition = _arrow.transform.localPosition;

        private void OnDestroy() =>
            _arrowTween?.Kill();

        public void Show()
        {
            ResetArrow();

            _arrow.SetActive(true);

            if (_duration <= 0)
                return;

            _arrowTween = _arrow.transform
                .DOLocalMoveY(_startPosition.y + _moveY, _duration)
                .SetEase(_ease)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void Hide()
        {
            ResetArrow();

            _arrow.SetActive(false);
        }

        private void ResetArrow()
        {
            _arrowTween?.Kill();
            _arrowTween = null;

            _arrow.transform.localPosition = _startPosition;
        }
    }
}
