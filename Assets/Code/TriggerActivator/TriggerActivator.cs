using Code.Movement;
using Code.Triggers;
using Code.Units;
using UniRx;
using UnityEngine;

namespace Code.TriggerActivator
{
    public sealed class TriggerActivator : MonoBehaviour
    {
        private readonly CompositeDisposable _disposables = new();
        
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform _center;
        [SerializeField] private float _radius;

        private IPlayer _player;
        private IMovement _movement;

        private void Awake()
        {
            _player = GetComponent<IPlayer>();
            _movement = GetComponent<IMovement>();
        }

        private void OnEnable() =>
            _movement.IsMoving.Subscribe(isMoving =>
            {
                if (!isMoving)
                    Cast();
            }).AddTo(_disposables);

        private void Cast()
        {
            var colliders = Physics
                .OverlapSphere(_center.position, _radius, _layerMask);
            
            foreach (var other in colliders)
            {
                if (other.TryGetComponent(out Trigger trigger))
                {
                    trigger.ActivateBy(_player);
                }
            }
        }

        private void OnDisable() => 
            _disposables.Dispose();
    }
}