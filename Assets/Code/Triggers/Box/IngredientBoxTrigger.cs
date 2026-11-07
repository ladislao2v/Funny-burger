using Code.BurgerPlate;
using Code.Ingredients;
using Code.Services.TutorialService;
using Code.Units;
using Code.Units.Commands;
using UnityEngine;
using Zenject;

namespace Code.Triggers.Box
{
    public sealed class IngredientBoxTrigger : Trigger
    {
        [SerializeField] private IngredientType _ingredientType;

        private ITutorialIngredientFilter _tutorialIngredientFilter;

        public IngredientType IngredientType => _ingredientType;

        [Inject]
        private void Construct(ITutorialIngredientFilter tutorialIngredientFilter) => 
            _tutorialIngredientFilter = tutorialIngredientFilter;

        protected override bool TryInteractWith(IPlayer player)
        {
            if (!_tutorialIngredientFilter.CanTake(player.Plate, _ingredientType))
                return false;
            
            IBurgerPlateValidator validator = 
                new BurgerPlateValidator(player.Plate);

            if(!validator.Validate(_ingredientType))
                 return false;
            
            var addIngredientCommand = 
                new AddIngredientCommand(player.Plate, _ingredientType);
            
            player.Do(addIngredientCommand);

            return true;
        }
    }
}