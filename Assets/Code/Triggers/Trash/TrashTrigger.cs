using Code.Units;
using Code.Units.Commands;

namespace Code.Triggers.Trash
{
    public sealed class TrashTrigger : Trigger<IPlayer>
    {
        protected override bool TryInteractWith(IPlayer player)
        {
            if(player.Plate.IsEmpty)
                return false;

            var clearPlateCommand = 
                new ClearPlateCommand(player.Plate);

            player.Do(clearPlateCommand, Disable);

            return true;
        }
    }
}