using Code.Ingredients;
using Code.Services.GameDataService;

namespace Code.Services.PurchasedBoxesService
{
    public interface IPurchasedBoxesService : ISavable
    {
        bool IsPurchased(IngredientType ingredientType);
        void MarkPurchased(IngredientType ingredientType);
    }
}
