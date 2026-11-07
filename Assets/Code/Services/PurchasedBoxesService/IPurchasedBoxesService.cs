using System;
using Code.Ingredients;
using Code.Services.GameDataService;

namespace Code.Services.PurchasedBoxesService
{
    public interface IPurchasedBoxesService : ISavable
    {
        event Action<IngredientType> Purchased;

        bool IsPurchased(IngredientType ingredientType);
        void MarkPurchased(IngredientType ingredientType);
    }
}
