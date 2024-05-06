using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Model.Movement
{
    public class InputListener : MonoBehaviour
    {
        private IInput _input;
        private IMovable _movable;

        [Inject]
        private void Construct(IInput input)
        {
            _input = input;
            _movable = GetComponent<IMovable>();
        }

        private void FixedUpdate()
        {
            _movable.Move(_input.Direction);
        }
    }
}