using System;
using UnityEngine;

namespace Code.Triggers
{
    [RequireComponent(typeof(Collider))]
    public abstract class Trigger<TTriggerActivator> : MonoBehaviour
    {
        public event Action InteractionStarted;
        public event Action InteractionEnded;
        
        private void OnTriggerEnter(Collider other)
        {
            if(!IsActivator(other, out TTriggerActivator triggerActivator))
                return;
            
            if(!TryInteractWith(triggerActivator))
                return;
            
            InteractionStarted?.Invoke();
        }

        protected bool IsActivator(Collider other, out TTriggerActivator triggerActivator) => 
            other.TryGetComponent(out triggerActivator);

        protected void Disable() => InteractionEnded?.Invoke();
        protected abstract bool TryInteractWith(TTriggerActivator activator);
    }
}