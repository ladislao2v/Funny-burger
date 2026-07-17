using System;
using System.Collections.Generic;
using System.Linq;
using Code.Ingredients;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;

namespace Code.Services.PurchasedBoxesService
{
    public sealed class PurchasedBoxesService : IPurchasedBoxesService
    {
        private readonly HashSet<IngredientType> _purchasedIngredients = new();

        public string SaveKey => nameof(PurchasedBoxesService);

        public bool IsPurchased(IngredientType ingredientType) =>
            _purchasedIngredients.Contains(ingredientType);

        public void MarkPurchased(IngredientType ingredientType) =>
            _purchasedIngredients.Add(ingredientType);

        public void Load(IData data)
        {
            if (data == null)
                return;

            if (data is not PurchasedBoxesData purchasedBoxesData)
                throw new ArgumentException(nameof(data));

            _purchasedIngredients.Clear();

            if (purchasedBoxesData.PurchasedIngredients == null)
                return;

            foreach (IngredientType ingredientType in purchasedBoxesData.PurchasedIngredients)
                _purchasedIngredients.Add(ingredientType);
        }

        public IData Save() =>
            new PurchasedBoxesData(_purchasedIngredients.ToList());
    }
}
