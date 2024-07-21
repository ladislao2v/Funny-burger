using System.Collections.Generic;
using System.Linq;
using Code.Recipes;
using Code.Services.RecipeService;
using Code.Services.StaticDataService;
using Code.Services.WalletService;

namespace Code.Services.ShopService
{
    public class Shop : IShopService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IRecipeService _recipeService;
        private readonly IWalletService _walletService;

        public IEnumerable<Recipe> AllRecipes => _staticDataService.GetRecipes();
        public IEnumerable<Recipe> AvailableRecipes => PickAvailableRecipes();

        public Shop(IStaticDataService staticDataService, 
            IRecipeService recipeService, IWalletService walletService)
        {
            _staticDataService = staticDataService;
            _recipeService = recipeService;
            _walletService = walletService;
        }
        
        public bool TryBuy(Recipe recipe)
        {
            if (!_walletService.TrySpend(recipe.Price))
                return false;
            
            _recipeService.AddRecipe(recipe);

            return false;
        }

        private IEnumerable<Recipe> PickAvailableRecipes() =>
            AllRecipes
                .Where(x => x.Price <= _walletService.Money.Value);
    }
}