using System;
using System.Collections.Generic;
using Code.Constants;
using Code.Movement;
using UniRx;
using UnityEngine;

namespace Code.Effects.Units
{
    [RequireComponent(typeof(Animator))]
    public sealed class WalkView : MonoBehaviour
    {
        private IMovement _movement;
        private Animator _animator;
        private ICollection<IDisposable> _disposable;

        private void Awake()
        {
            _movement = GetComponent<IMovement>();
            _animator = GetComponent<Animator>();
            _disposable = new List<IDisposable>();
        }

        private void OnEnable()
        {
            _movement.IsMoving.Subscribe(value =>
            {
                _animator.SetBool(ChefAnimatorParametr.Moving, value);
            }).AddTo(_disposable);
        }

        private void OnDisable() => 
            _disposable.Clear();
    }
}