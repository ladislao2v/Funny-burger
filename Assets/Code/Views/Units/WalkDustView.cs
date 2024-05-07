using System;
using System.Collections.Generic;
using Code.Model.Movement;
using UniRx;
using UnityEngine;

namespace Code.Views.Units
{
    public class WalkDustView : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _dust;

        private IMovable _movable;
        private ICollection<IDisposable> _disposables;

        private void Awake()
        {
            _movable = GetComponent<IMovable>();
            _disposables = new List<IDisposable>();
        }

        private void OnEnable()
        {
            _movable.IsMoving.Subscribe(value =>
            {
                if(value)
                    _dust.ForEach(x => x.Play());
                else
                    _dust.ForEach(x => x.Stop());
                
            }).AddTo(_disposables);
        }

        private void OnDisable() => 
            _disposables.Clear();
    }
}