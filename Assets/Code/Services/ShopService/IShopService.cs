using System.Collections.Generic;
using Code.Recipes;

namespace Code.Services.ShopService
{
    public interface IShopService
    {
        public IEnumerable<Recipe>  AllRecipes { get; }
        public IEnumerable<Recipe>  AvailableRecipes { get; }

        public bool TryBuy(Recipe recipe);
    }
}