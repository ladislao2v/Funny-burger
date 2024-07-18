using Code.BurgerPlate;
using Code.Goods;
using Code.Units;
using Code.Units.Commands;
using UnityEngine;

namespace Code.Kitchen.Box
{
    [RequireComponent(typeof(SphereCollider))]
    public sealed class Box : Interactor<IPlayer>
    {
        [SerializeField] private IngredientType _ingredientType;
        protected override bool TryInteractWith(IPlayer player)
        {
            IBurgerPlateValidator validator = 
                new BurgerPlateValidator(player.BurgerPlate);

            if(!validator.Validate(_ingredientType))
                return false;
            
            var addIngredientCommand = 
                new AddIngredientCommand(player.BurgerPlate, _ingredientType);
            
            player.Do(addIngredientCommand, Disable);

            return true;
        }
    }
}