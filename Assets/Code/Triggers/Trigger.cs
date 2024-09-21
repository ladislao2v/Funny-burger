using System;
using Code.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Triggers
{
    [RequireComponent(typeof(Collider))]
    public abstract class Trigger : MonoBehaviour
    {
        public event Action InteractionStarted;
        public event Action InteractionEnded;
        
        public void ActivateBy(IPlayer player)
        {
            if(!TryInteractWith(player))
                return;
            
            InteractionStarted?.Invoke();
        }

        protected void Disable() => InteractionEnded?.Invoke();
        protected abstract bool TryInteractWith(IPlayer player);
    }
}