using Code.Extensions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Services.Input
{
    public sealed class JoystickInput : IInput, ITickable
    {
        private readonly Joystick _joystick;

        private Vector3 _direction;
        public Vector3 Direction => _direction;

        public JoystickInput(Joystick joystick)
        {
            _joystick = joystick;
        }

        public void Enable() => 
            _joystick.enabled = true;

        public void Disable() => 
            _joystick.enabled = false;

        public void Tick() => 
            _direction = _joystick
                .Direction
                .ToVector3()
                .normalized;
    }
}