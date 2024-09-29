using Code.Services.Input;
using Code.Units;
using UnityEngine;
using Zenject;

namespace Code.Movement
{
    public sealed class FixedPlayerRouter : Router
    {
        private IInput _input;
        private IPlayer _player;

        [Inject]
        private void Construct(IInput input)
        {
            _input = input;
        }

        private void Awake()
        {
            _player = GetComponent<IPlayer>();
            
            _player.TaskStarted += _input.Disable;
            _player.TaskEnded += _input.Enable;
        }

        public void FixedUpdate()
        {
            Rout(_player.Movement, _input.Direction, _player.Config.Speed);
        }

        public void OnDestroy()
        {
            _player.TaskStarted -= _input.Disable;
            _player.TaskEnded -= _input.Enable;
        }

        protected override void Rout(IMovement movement, Vector3 direction, float speed) => 
            movement.Move(direction, speed);
    }
}