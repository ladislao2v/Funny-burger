using System;
using System.Collections.Generic;
using Code.Configs;
using Code.Services.ConfigProvider;
using Code.Services.GameDataService;
using Code.Services.LevelService;
using Code.Services.RecipeService;
using Code.Services.WalletService;
using UniRx;
using Zenject;

namespace Code.Services.ShopService
{
    public sealed class RecipeShop : IRecipeShopService, IInitializable, IDisposable
    {
        private readonly IConfigProvider _configProvider;
        private readonly ILevelService _levelService;
        private readonly IRecipeService _recipeService;
        private readonly IWalletService _walletService;
        private readonly CompositeDisposable _disposables = new();

        public IEnumerable<RecipeConfig> AllRecipes => _configProvider.GetRecipes();

        public event Action Updated;

        public RecipeShop(IConfigProvider configProvider, ILevelService levelService,
            IRecipeService recipeService, IWalletService walletService)
        {
            _configProvider = configProvider;
            _levelService = levelService;
            _recipeService = recipeService;
            _walletService = walletService;
        }

        public void Initialize()
        {
            _walletService.Money.Subscribe(OnMoneyChanged).AddTo(_disposables);
            _levelService.LevelChanged += OnLevelChanged;
        }

        public bool TryBuy(RecipeConfig recipeConfig)
        {
            if (!_walletService.TrySpend(recipeConfig.Price))
                return false;

            if (_levelService.Current < recipeConfig.RequiredLevel)
                return false;

            if (_recipeService.Has(recipeConfig))
                return false;

            return true;
        }

        public void Buy(RecipeConfig recipeConfig)
        {
            if(!TryBuy(recipeConfig))
                return;

            _recipeService.AddRecipe(recipeConfig);
            _walletService.Spend(recipeConfig.Price);
        }

        public bool IsBought(RecipeConfig recipe) => 
            _recipeService.Has(recipe);

        public void Dispose()
        {
            _disposables.Dispose();
            _levelService.LevelChanged -= OnLevelChanged;
        }

        private void OnMoneyChanged(int value) => Updated?.Invoke();
        private void OnLevelChanged(int current, int next) => Updated?.Invoke();
    }
}