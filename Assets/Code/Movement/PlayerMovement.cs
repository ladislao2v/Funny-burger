using System;
using Code.Configs;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Movement
{
    public sealed class PlayerMovement : MonoBehaviour, IMovement
    {
        [SerializeField] private Transform _model;
        
        private CharacterController _characterController;
        private ReactiveProperty<bool> _isMoving = new();
        public IReactiveProperty<bool> IsMoving => _isMoving;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(Vector3 direction, float speed)
        {
            IsMoving.Value = direction.magnitude != 0;
            
            if(IsMoving.Value == false)
                return;
            
            _model.LookAt(_model.position + direction);
            _characterController.Move(direction * (speed * Time.fixedDeltaTime));
        }
    }
}