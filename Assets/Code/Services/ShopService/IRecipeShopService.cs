using System;
using System.Collections.Generic;
using Code.Configs;

namespace Code.Services.ShopService
{
    public interface IRecipeShopService
    {
        IEnumerable<RecipeConfig>  AllRecipes { get; }

        event Action Updated;

        bool TryBuy(RecipeConfig recipeConfig);
        void Buy(RecipeConfig recipeConfig);
        bool IsBought(RecipeConfig recipe);
    }
}