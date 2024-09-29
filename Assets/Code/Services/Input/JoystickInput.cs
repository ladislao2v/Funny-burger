using Code.Extensions;
using UnityEngine;
using Zenject;

namespace Code.Services.Input
{
    public sealed class JoystickInput : IInput, ITickable
    {
        private readonly IJoystickProvider _joystickProvider;

        private Vector3 _direction;
        public Vector3 Direction => _direction;

        public bool IsInit => _joystickProvider != null;
        
        public JoystickInput(IJoystickProvider joystickProviderProvider) => 
            _joystickProvider = joystickProviderProvider;

        public void Enable() => 
            _joystickProvider.Joystick.gameObject.SetActive(true);

        public void Disable()
        {
            _joystickProvider.Joystick.OnPointerUp(null);
            _joystickProvider.Joystick.gameObject.SetActive(false);
            _direction = Vector3.zero;
        }

        public void Tick()
        {
            if(_joystickProvider.Joystick == null)
                return;
            
            if(!_joystickProvider.Joystick.gameObject.activeInHierarchy)
                return;
            
            _direction = _joystickProvider
                .Joystick
                .Direction
                .normalized
                .ToVector3();
        }
    }
}