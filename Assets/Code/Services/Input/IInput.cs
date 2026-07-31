using System;
using UnityEngine;

namespace Code.Services.Input
{
    public interface IInput
    {
        Vector3 Direction { get; }
        bool IsInit { get; }
        
        event Action InputStarted;
        event Action InputEnded;

        void Enable();
        void Disable();
    }
}