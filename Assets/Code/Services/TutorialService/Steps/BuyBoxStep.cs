using Code.Effects.Box;
using Code.Ingredients;
using Code.Services.PurchasedBoxesService;

namespace Code.Services.TutorialService.Steps
{
    public sealed class BuyBoxStep : TutorialStep
    {
        private readonly IPurchasedBoxesService _purchasedBoxesService;
        private readonly IngredientType _ingredientType;

        public BuyBoxStep(TutorialArrowView arrow, IPurchasedBoxesService purchasedBoxesService,
            IngredientType ingredientType) : base(arrow)
        {
            _purchasedBoxesService = purchasedBoxesService;
            _ingredientType = ingredientType;
        }

        protected override void OnStarted()
        {
            if (_purchasedBoxesService.IsPurchased(_ingredientType))
            {
                Complete();
                return;
            }

            _purchasedBoxesService.Purchased += OnPurchased;
        }

        protected override void OnEnded() =>
            _purchasedBoxesService.Purchased -= OnPurchased;

        private void OnPurchased(IngredientType ingredientType)
        {
            if (ingredientType == _ingredientType)
                Complete();
        }
    }
}
