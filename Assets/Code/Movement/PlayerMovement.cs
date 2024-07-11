using Code.Configs;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Movement
{
    public sealed class PlayerMovement : MonoBehaviour, IMovable
    {
        private ChefConfig _playerConfig;
        private Transform _transform;
        private CharacterController _characterController;
        private ReactiveProperty<bool> _isMoving = new();
        
        public IReactiveProperty<bool> IsMoving => _isMoving;

        [Inject]
        private void Construct(ChefConfig chefConfig)
        {
            _playerConfig = chefConfig;
            _transform = transform;
            _characterController = GetComponent<CharacterController>();
        }
        
        public void Move(Vector3 direction)
        {
            IsMoving.Value = direction.magnitude != 0;
            
            if(IsMoving.Value == false)
                return;
            
            _transform.LookAt(_transform.position + direction);
            _characterController.Move(direction * _playerConfig.Speed * Time.fixedDeltaTime);
        }
    }
}