using System;
using System.Collections.Generic;
using Code.Ingredients;
using Code.Services.GameDataService;

namespace Code.Services.GameDataService.Data
{
    [Serializable]
    public class PurchasedBoxesData : IData
    {
        public List<IngredientType> PurchasedIngredients { get; set; } = new();

        public PurchasedBoxesData() { }

        public PurchasedBoxesData(List<IngredientType> purchasedIngredients)
        {
            PurchasedIngredients = purchasedIngredients;
        }
    }
}
