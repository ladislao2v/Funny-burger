using System.Collections.Generic;
using Code.BurgerPlate;
using Code.Effects.Box;
using Code.Ingredients;

namespace Code.Services.TutorialService.Steps
{
    public sealed class TakeIngredientStep : TutorialStep
    {
        private readonly IBurgerPlate _plate;
        private readonly IngredientType _ingredientType;

        public TakeIngredientStep(TutorialArrowView arrow, IBurgerPlate plate, IngredientType ingredientType)
            : base(arrow)
        {
            _plate = plate;
            _ingredientType = ingredientType;
        }

        protected override void OnStarted()
        {
            _plate.IngredientAdded += OnIngredientAdded;
            _plate.IngredientsAdded += OnIngredientsAdded;

            TryComplete();
        }

        protected override void OnEnded()
        {
            _plate.IngredientAdded -= OnIngredientAdded;
            _plate.IngredientsAdded -= OnIngredientsAdded;
        }

        private void OnIngredientAdded(IngredientType _) =>
            TryComplete();

        private void OnIngredientsAdded(IEnumerable<IngredientType> _) =>
            TryComplete();

        private void TryComplete()
        {
            if (_plate.Contains(_ingredientType))
                Complete();
        }
    }
}
