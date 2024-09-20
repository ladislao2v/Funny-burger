using System;
using System.Collections.Generic;
using Code.Configs;
using Code.UI.Shop;

namespace Code.Services.ShopService
{
    public interface IRecipeShopService
    {
        IEnumerable<RecipeConfig>  AllRecipes { get; }

        event Action Updated;

        ItemState TryBuy(RecipeConfig recipeConfig);
        void Buy(RecipeConfig recipeConfig);
        bool IsBought(RecipeConfig recipe);
    }
}