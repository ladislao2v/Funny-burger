using UniRx;
using UnityEngine;

namespace Code.Movement
{
    public sealed class PlayerMovement : MonoBehaviour, IMovement
    {
        [SerializeField] private Transform _model;
        
        private CharacterController _characterController;
        private ReactiveProperty<bool> _isMoving = new();
        
        private float _heightAboveSurface;
        private Quaternion _modelStartRotation;
        
        public IReactiveProperty<bool> IsMoving => _isMoving;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _modelStartRotation = _model.rotation;
        }

        private void Start()
        {
            _heightAboveSurface = transform.position.y;
        }

        public void Teleport(Vector3 position)
        {
            _isMoving.Value = false;
            _model.rotation = _modelStartRotation;
            
            _characterController.enabled = false;
            transform.position = position;
            _characterController.enabled = true;
        }

        public void Move(Vector3 direction, float speed)
        {
            bool isMove = direction.magnitude != 0;

            if (_isMoving.Value != isMove)
                _isMoving.Value = isMove;

            if (IsMoving.Value == false)
                return;

            _model.LookAt(_model.position + direction);
            _characterController.Move(direction * (speed * Time.fixedDeltaTime));
            transform.position = new Vector3(transform.position.x, _heightAboveSurface, transform.position.z);
        }
    }
}