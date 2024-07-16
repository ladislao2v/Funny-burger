using Code.Configs;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Movement
{
    public sealed class PlayerMovement : MonoBehaviour, IMovement
    {
        [SerializeField] private Transform _model;
        
        private ChefConfig _playerConfig;
        private CharacterController _characterController;
        private ReactiveProperty<bool> _isMoving = new();
        
        public IReactiveProperty<bool> IsMoving => _isMoving;

        [Inject]
        private void Construct(ChefConfig chefConfig)
        {
            _playerConfig = chefConfig;
            _characterController = GetComponent<CharacterController>();
        }
        
        public void Move(Vector3 direction)
        {
            IsMoving.Value = direction.magnitude != 0;
            
            if(IsMoving.Value == false)
                return;
            
            _model.LookAt(_model.position + direction);
            _characterController.Move(direction * _playerConfig.Speed * Time.fixedDeltaTime);
        }
    }
}