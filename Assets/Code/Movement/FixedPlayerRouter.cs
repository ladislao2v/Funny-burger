using System;
using Code.Services.Input;
using Code.Units;
using UnityEditor.VersionControl;
using UnityEngine;
using Zenject;

namespace Code.Movement
{
    public sealed class FixedPlayerRouter : Router, IInitializable, IFixedTickable, IDisposable
    {
        private readonly IInput _input;
        private readonly IPlayer _player;
        
        public FixedPlayerRouter(IInput input, IPlayer player)
        {
            _input = input;
            _player = player;
        }

        protected override void Rout(IMovement movement, Vector3 direction, float speed) => 
            movement.Move(direction, speed);

        public void Initialize()
        {
            _player.TaskStarted += _input.Disable;
            _player.TaskEnded += _input.Enable;
        }

        public void FixedTick()
        {
            Rout(_player.Movement, _input.Direction, _player.Config.Speed);
        }

        public void Dispose()
        {
            _player.TaskStarted -= _input.Disable;
            _player.TaskEnded -= _input.Enable;
        }
    }
}