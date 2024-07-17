using System;
using System.Collections.Generic;
using Code.Movement;
using UniRx;
using UnityEngine;

namespace Code.Effects.Units
{
    public sealed class WalkDustView : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _dust;

        private IMovement _movement;
        private ICollection<IDisposable> _disposables;

        private void Awake()
        {
            _movement = GetComponent<IMovement>();
            _disposables = new List<IDisposable>();
        }

        private void OnEnable()
        {
            _movement.IsMoving.Subscribe(value =>
            {
                _dust.ForEach(x =>
                {
                    if(value)
                        x.Play();
                    else
                        x.Stop();
                });
                
            }).AddTo(_disposables);
        }

        private void OnDisable() => 
            _disposables.Clear();
    }
}