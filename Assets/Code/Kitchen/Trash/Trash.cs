using Code.Units;
using Code.Units.Commands;

namespace Code.Kitchen.Trash
{
    public sealed class Trash : Trigger<IPlayer>
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