using UniRx;
using UnityEngine;

namespace Code.Services.Input
{
    public interface IInput
    {
        Vector3 Direction { get; }

        void Enable();
        void Disable();
    }
}