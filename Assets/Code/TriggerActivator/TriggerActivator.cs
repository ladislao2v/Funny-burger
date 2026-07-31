using System.Collections.Generic;
using System.Linq;
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
        private readonly Collider[] _overlapResults = new Collider[10];
        private readonly List<Trigger> _cachedTriggers = new(10);


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
            int count = Physics.OverlapSphereNonAlloc(_center.position, _radius, _overlapResults, _layerMask);
    
            if (count == 0)
                return;

            var target = _overlapResults
                .Take(count)
                .Where(x => x != null && x.TryGetComponent(out Trigger _))
                .OrderBy(x => (x.transform.position - transform.position).sqrMagnitude)
                .FirstOrDefault();
    
            if (target == null)
                return;
            
            target.GetComponents(_cachedTriggers);
    
            foreach (var trigger in _cachedTriggers) 
                trigger.ActivateBy(_player);
        
            _cachedTriggers.Clear();
            System.Array.Clear(_overlapResults, 0, count);
        }

        private void OnDisable() => 
            _disposables.Dispose();
    }
}