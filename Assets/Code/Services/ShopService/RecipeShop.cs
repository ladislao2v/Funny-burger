﻿using System;
using System.Collections.Generic;
using Code.Configs;
using Code.Services.ConfigProvider;
using Code.Services.LevelService;
using Code.Services.RecipeService;
using Code.Services.ResourceStorage;
using Code.UI.Shop;
using UniRx;
using Zenject;
using static Code.Services.ResourceStorage.ResourceType;

namespace Code.Services.ShopService
{
    public sealed class RecipeShop : IShopService, IInitializable, IDisposable
    {
        private readonly IConfigProvider _configProvider;
        private readonly ILevelService _levelService;
        private readonly IRecipeService _recipeService;
        private readonly IResourceStorage _resourceStorage;
        private readonly CompositeDisposable _disposables = new();

        public IEnumerable<IItem> AllItems => _configProvider.GetRecipes();

        public event Action Updated;

        public RecipeShop(IConfigProvider configProvider, ILevelService levelService,
            IRecipeService recipeService, IResourceStorage resourceStorage)
        {
            _configProvider = configProvider;
            _levelService = levelService;
            _recipeService = recipeService;
            _resourceStorage = resourceStorage;
        }

        public void Initialize()
        {
            _resourceStorage
                .GetWallet(Coin).Money.
                Subscribe(OnMoneyChanged)
                .AddTo(_disposables);
            
            _levelService.LevelChanged += OnLevelChanged;
        }

        public ItemState TryBuy(IItem item)
        {
            if (item is not RecipeConfig recipeConfig)
                throw new ArgumentException(nameof(item));
            
            if (_recipeService.Has(recipeConfig))
                return (ItemState.Selected);

            if (_levelService.Current < recipeConfig.RequiredLevel)
                return (ItemState.Level);
            
            if (!_resourceStorage
                    .GetWallet(Coin)
                    .TrySpend(recipeConfig.Price))
                return ItemState.Money;


            return (ItemState.CanBuy);
        }

        public void Buy(IItem item)
        {
            if (item is not RecipeConfig recipeConfig)
                throw new ArgumentException(nameof(item));
            
            if(TryBuy(recipeConfig) != ItemState.CanBuy)
                return;

            _recipeService.AddRecipe(recipeConfig);
            _resourceStorage
                .GetWallet(Coin)
                .Spend(recipeConfig.Price);
        }

        public bool IsBought(IItem item)
        {
            if (item is not RecipeConfig recipeConfig)
                throw new ArgumentException(nameof(item));
            
            return _recipeService.Has(recipeConfig);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _levelService.LevelChanged -= OnLevelChanged;
        }

        private void OnMoneyChanged(int value) => Updated?.Invoke();
        private void OnLevelChanged(int current, int next) => Updated?.Invoke();
    }
}