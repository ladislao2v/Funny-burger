using System;
using Code.BurgerPlate;
using Code.Ingredients;
using Code.Units;
using Code.Units.Commands;
using UniRx;
using UnityEngine;

namespace Code.Triggers.Box
{
    public sealed class IngredientBoxTrigger : Trigger
    {
        [SerializeField] private IngredientType _ingredientType;
        
        private IDisposable _timer;

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