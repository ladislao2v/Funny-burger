 using Code.Units;
using UnityEngine;

namespace Code.Triggers
{
    public abstract class PlayerExitTrigger : Trigger<IPlayer>
    {
        private void OnTriggerExit(Collider other)
        {
            if(!IsActivator(other, out IPlayer player))
                return;
            
            player.Reset();
            Disable();
        }
    }
}