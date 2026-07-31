using System;
using Code.Units;
using UnityEngine;

namespace Code.Triggers
{
    [RequireComponent(typeof(Collider))]
    public abstract class Trigger : MonoBehaviour
    {
        private IPlayer _player;
        
        public event Action InteractionStarted;
        public event Action InteractionEnded;
        
        public void ActivateBy(IPlayer player)
        {
            if(!TryInteractWith(player))
                return;
            
            InteractionStarted?.Invoke();

            if (!player.IsBusy)
                return;
            
            _player = player;
            _player.TaskEnded += OnPlayerTaskEnded;
        }

        private void OnPlayerTaskEnded()
        {
            _player.TaskEnded -= OnPlayerTaskEnded;
            _player = null;
            
            InteractionEnded?.Invoke();
        }

        protected abstract bool TryInteractWith(IPlayer player);
    }
}