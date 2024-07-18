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

            var burger = _uncookedBurger.transform;
            var startPosition = burger.localPosition;

            _sequence = CreateSequence(burger, startPosition);
        }

        private Sequence CreateSequence(Transform transform, Vector3 startPosition)
        {
            Sequence sequence = DOTween.Sequence();
            
            sequence
                .Append(transform
                    .DOMoveY(transform.localPosition.y + _height, _duration)
                    .SetEase(_jumpEase))
                .Append(transform
                    .DORotate(new Vector3(_angle, 0, 0), _duration)
                    .SetEase(_rotateEase))
                .Append(transform
                    .DOMoveY(transform.localPosition.y - _height, _duration)
                    .SetEase(_jumpEase))
                .AppendCallback(() =>
                {
                    _cookedBurger.SetActive(true);
                    _uncookedBurger.SetActive(false);
                    transform.localPosition = startPosition;
                    transform.rotation = Quaternion.identity;
                });

            return sequence;
        }

        private void OnExit()
        {
            _cookedBurger.SetActive(false);
            _sequence.Kill();
        }
    }
}