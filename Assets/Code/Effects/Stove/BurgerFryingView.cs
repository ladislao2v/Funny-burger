using System;
using Code.Kitchen;
using Code.Units;
using DG.Tweening;
using UnityEngine;

namespace Code.Effects.Stove
{
    public class BurgerFryingView : MonoBehaviour
    {
        [SerializeField] private Interactor<IPlayer> _interactor;
        [SerializeField] private GameObject _uncookedBurger;
        [SerializeField] private GameObject _cookedBurger;
        [SerializeField] private float _height;
        [SerializeField] private float _angle;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _jumpEase = Ease.OutQuad;
        [SerializeField] private Ease _rotateEase = Ease.InOutSine;

        private Sequence _sequence;

        private void Awake()
        {
            _interactor.Enter += OnEnter;
            _interactor.Exit += OnExit;
        }

        private void OnDestroy()
        {
            _interactor.Enter -= OnEnter;
            _interactor.Exit -= OnExit;
        }

        private void OnEnter()
        {
            _uncookedBurger.SetActive(true);
            
            _sequence = DOTween.Sequence();

            var burger = _uncookedBurger.transform;
            var startPosition = burger.localPosition;

            _sequence
                .Append(burger
                    .DOMoveY(burger.localPosition.y + _height, _duration)
                    .SetEase(_jumpEase))
                .Append(burger
                    .DORotate(new Vector3(_angle, 0, 0), _duration)
                    .SetEase(_rotateEase))
                .Append(burger
                    .DOMoveY(burger.localPosition.y - _height, _duration)
                    .SetEase(_jumpEase))
                .AppendCallback(() =>
                {
                    _cookedBurger.SetActive(true);
                    _uncookedBurger.SetActive(false);
                    burger.localPosition = startPosition;
                    burger.rotation = Quaternion.identity;
                });
        }

        private void OnExit()
        {
            _cookedBurger.SetActive(false);
            _sequence.Kill();
        }
    }
}