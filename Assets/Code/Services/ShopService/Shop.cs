using System.Collections.Generic;
using System.Linq;
using Code.Recipes;
using Code.Services.ConfigProvider;
using Code.Services.RecipeService;
using Code.Services.WalletService;

namespace Code.Services.ShopService
{
    public class Shop : IShopService
    {
        private readonly IConfigProvider _configProvider;
        private readonly IRecipeService _recipeService;
        private readonly IWalletService _walletService;

        public IEnumerable<Recipe> AllRecipes => _configProvider.GetRecipes();
        public IEnumerable<Recipe> AvailableRecipes => PickAvailableRecipes();

        public Shop(IConfigProvider configProvider, 
            IRecipeService recipeService, IWalletService walletService)
        {
            _configProvider = configProvider;
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