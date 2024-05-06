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
            _direction = new();
            _joystick = joystick;
        }

        public void Enable() => 
            _joystick.enabled = true;

        public void Disable() => 
            _joystick.enabled = false;

        public void Tick() => 
            _direction = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;
    }
}