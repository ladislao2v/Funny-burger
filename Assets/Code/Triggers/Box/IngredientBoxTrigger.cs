using Code.BurgerPlate;
using Code.Ingredients;
using Code.Units;
using Code.Units.Commands;
using UnityEngine;

namespace Code.Triggers.Box
{
    public sealed class IngredientBoxTrigger : Trigger<IPlayer>
    {
        [SerializeField] private IngredientType _ingredientType;
        protected override bool TryInteractWith(IPlayer player)
        {
            IBurgerPlateValidator validator = 
                new BurgerPlateValidator(player.Plate);

            if(!validator.Validate(_ingredientType))
                return false;
            
            var addIngredientCommand = 
                new AddIngredientCommand(player.Plate, _ingredientType);
            
            player.Do(addIngredientCommand, Disable);

            return true;
        }
    }
}