using System;
using UnityEngine;

namespace Code.Kitchen
{
    public abstract class Interactor<TTriggerActivator> : MonoBehaviour
    {
        public event Action Enter;
        public event Action Exit;
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out TTriggerActivator triggerActivator) == false)
                return;
            
            if(!TryInteractWith(triggerActivator))
                return;
            
            Enter?.Invoke();
        }
        
        protected void Disable() => Exit?.Invoke();
        protected abstract bool TryInteractWith(TTriggerActivator activator);
    }
}