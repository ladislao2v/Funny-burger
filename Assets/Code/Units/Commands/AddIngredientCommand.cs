using Code.BurgerPlate;
using Code.Goods;
using Code.Ingredients;

namespace Code.Units.Commands
{
    public sealed class AddIngredientCommand : ICommand
    {
        private readonly IBurgerPlate _playerBurgerPlate;
        private readonly IngredientType _ingredientType;

        public AddIngredientCommand(IBurgerPlate playerBurgerPlate, IngredientType ingredientType)
        {
            _playerBurgerPlate = playerBurgerPlate;
            _ingredientType = ingredientType;
        }

        public void Execute()
        {
            _playerBurgerPlate.Add(_ingredientType);
        }
    }
}