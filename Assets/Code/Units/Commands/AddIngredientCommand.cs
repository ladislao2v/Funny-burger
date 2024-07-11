using Code.BurgerPlate;
using Code.Ingredients;

namespace Code.Units.Commands
{
    public class AddIngredientCommand : ICommand
    {
        private readonly IBurgerPlate _playerBurgerPlate;
        private readonly Ingredient _ingredient;

        public AddIngredientCommand(IBurgerPlate playerBurgerPlate, Ingredient ingredient)
        {
            _playerBurgerPlate = playerBurgerPlate;
            _ingredient = ingredient;
        }

        public void Execute()
        {
            _playerBurgerPlate.Add(_ingredient);
        }
    }
}