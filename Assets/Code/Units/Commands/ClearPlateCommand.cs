using Code.BurgerPlate;

namespace Code.Units.Commands
{
    public sealed class ClearPlateCommand : ICommand
    {
        private readonly IBurgerPlate _burgerPlate;

        public ClearPlateCommand(IBurgerPlate burgerPlate) => 
            _burgerPlate = burgerPlate;

        public void Execute() => 
            _burgerPlate.Clear();
    }
}