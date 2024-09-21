using Code.Units;

namespace Code.Triggers.Trash
{
    public sealed class TrashTrigger : Trigger
    {
        protected override bool TryInteractWith(IPlayer player)
        {
            if(player.Plate.IsEmpty)
                return false;

            player.Plate.Clear();

            return true;
        }
    }
}