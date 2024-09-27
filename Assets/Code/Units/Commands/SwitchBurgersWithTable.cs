using System.Linq;
using Code.BurgerPlate;

namespace Code.Units.Commands
{
    public sealed class SwitchBurgersWithTable : ICommand
    {
        private readonly IBurgerPlate _playerPlate;
        private readonly IBurgerPlate _tablePlate;

        public SwitchBurgersWithTable(IBurgerPlate playerPlate, IBurgerPlate tablePlate)
        {
            _playerPlate = playerPlate;
            _tablePlate = tablePlate;
        }

        public void Execute()
        {
            var playerBurger= _playerPlate
                .Ingredients
                .ToArray();
            var tableBurger = _tablePlate
                .Ingredients
                .ToArray();
            
            _playerPlate.Clear();
            _tablePlate.Clear();
            
            _playerPlate.AddRange(tableBurger);
            _tablePlate.AddRange(playerBurger);
        }
    }
}