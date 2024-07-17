using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Triggers
{
    public abstract class ObservableTrigger<TTriggerActivator> : MonoBehaviour
    {
        private async void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out TTriggerActivator triggerActivator) == false)
                return;
            
            await InteractWith(triggerActivator);
        }

        protected abstract UniTask InteractWith(TTriggerActivator activator);
    }
}