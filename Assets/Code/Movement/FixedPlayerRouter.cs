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

        private void Awake() => 
            _player = GetComponent<IPlayer>();

        public void FixedUpdate()
        {
            if (_player.IsBusy)
            {
                if (_input.Direction != Vector3.zero)
                    _player.Reset();

                return;
            }
            
            Rout(_player.Movement, _input.Direction, _player.Config.Speed);
        }

        protected override void Rout(IMovement movement, Vector3 direction, float speed) => 
            movement.Move(direction, speed);
    }
}
