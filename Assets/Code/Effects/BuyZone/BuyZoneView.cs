using Code.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Effects.BuyZone
{
    public class BuyZoneView : View
    {
        private const float EndValue = 0.5f;
        private const float Duration = 1.25f;

        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private Image _zoneImage;
        
        private Tween _animation;

        public void Construct(int cost) => 
            _cost.text = cost.ToString();

        private void Awake() =>
            _animation = _zoneImage
                .DOFade(EndValue, Duration)
                .SetEase(Ease.InSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetAutoKill(false)
                .Pause();

        private void OnEnable() => _animation.Restart();

        private void OnDisable() => _animation.Pause();
    }
}