using Code.BurgerPlate;
using Code.Ingredients;

namespace Code.Units.Commands
{
    public sealed class AddIngredientCommand : ICommand
    {
        private readonly IBurgerPlate _plate;
        private readonly IngredientType _ingredientType;

        public AddIngredientCommand(IBurgerPlate plate, IngredientType ingredientType)
        {
            _plate = plate;
            _ingredientType = ingredientType;
        }

        public void Execute()
        {
            _plate.Add(_ingredientType);
        }
    }
}