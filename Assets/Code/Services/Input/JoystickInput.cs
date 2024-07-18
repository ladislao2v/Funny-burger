using Code.Extensions;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
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

        public void Enable()
        {
            _joystick.gameObject.SetActive(true);
        }

        public void Disable()
        {
            _joystick.OnPointerUp(null);
            _joystick.gameObject.SetActive(false);
            _direction = Vector3.zero;
        }

        public void Tick()
        {
            if(!_joystick.gameObject.activeInHierarchy)
                return;
            
            _direction = _joystick
                .Direction
                .ToVector3()
                .normalized;
        }
    }
}