using Code.Units;
using Code.Units.Commands;

namespace Code.Kitchen.Trash
{
    public sealed class Trash : Interactor<IPlayer>
    {
        protected override bool TryInteractWith(IPlayer player)
        {
            if(player.BurgerPlate.IsEmpty)
                return false;

            var clearPlateCommand = 
                new ClearPlateCommand(player.BurgerPlate);
            
            player.Do(clearPlateCommand, Disable);

            return true;
        }
    }
}