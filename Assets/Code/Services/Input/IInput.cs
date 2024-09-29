using UnityEngine;

namespace Code.Services.Input
{
    public interface IInput
    {
        Vector3 Direction { get; }
        bool IsInit { get; }

        void Enable();
        void Disable();
    }
}