using UnityEngine;

namespace Code.Triggers
{
    public abstract class ObservableTrigger<TTriggerActivator> : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out TTriggerActivator triggerActivator) == false)
                return;
            
            InteractWith(triggerActivator);
        }

        protected abstract void InteractWith(TTriggerActivator activator);
    }
}