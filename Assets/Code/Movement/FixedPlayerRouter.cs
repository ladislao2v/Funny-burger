using Code.Services.Input;
using Code.Units;
using UnityEngine;
using Zenject;

namespace Code.Movement
{
    public sealed class FixedPlayerRouter : Router, IFixedTickable
    {
        private readonly IInput _input;
        private readonly IPlayer _player;
        
        public FixedPlayerRouter(IInput input, IPlayer player)
        {
            _input = input;
            _player = player;
        }

        public void FixedTick() => 
            Rout(_player.Movement, _input.Direction, _player.Config.Speed);

        protected override void Rout(IMovement movement, Vector3 direction, float speed) => 
            movement.Move(direction, speed);
    }
}