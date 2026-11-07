using System.Collections.Generic;
using Code.Movement;
using UniRx;
using UnityEngine;

namespace Code.Effects.Units
{
    public sealed class WalkDustView : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _dust;

        private readonly CompositeDisposable _disposables = new();

        private IMovement _movement;

        private void Awake() => 
            _movement = GetComponent<IMovement>();

        private void OnEnable()
        {
            _movement.IsMoving.Subscribe(value =>
            {
                foreach (var x in _dust)
                {
                    if (value)
                    {
                        x.Play();
                    }
                    else
                    {
                        x.Stop();
                    }
                }
            }).AddTo(_disposables);
        }

        private void OnDisable() => 
            _disposables.Clear();

        private void OnDestroy() => 
            _disposables.Dispose();
    }
}