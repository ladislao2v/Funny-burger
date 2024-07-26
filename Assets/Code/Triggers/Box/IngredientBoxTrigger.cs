using Code.BurgerPlate;
using Code.Goods;
using Code.Kitchen;
using Code.Units;
using Code.Units.Commands;
using UnityEngine;

namespace Code.Triggers.Box
{
    [RequireComponent(typeof(SphereCollider))]
    public sealed class Box : Trigger<IPlayer>
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