using Code.Services.Input;
using Code.Units;
using UnityEngine;
using Zenject;

namespace Code.Movement
{
    public sealed class FixedPlayerRouter : IPlayerRouter, IFixedTickable
    {
        private readonly IInput _input;
        private readonly IPlayer _player;
        
        private FixedPlayerRouter(IInput input, IPlayer player)
        {
            _input = input;
            _player = player;
        }

        public void FixedTick() => 
            Rout(_player.Movement, _input.Direction);

        public void Rout(IMovement movement, Vector3 direction) => 
            movement.Move(direction);
    }
}