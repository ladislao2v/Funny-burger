using System;
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
    public abstract class ShopService : IShopService, IInitializable, IDisposable
    {
        private readonly IConfigProvider _configProvider;
        private readonly ILevelService _levelService;
        private readonly IResourceStorage _resourceStorage;
        private readonly CompositeDisposable _disposables = new();

        public IEnumerable<ShopItemConfig> AllItems => _configProvider.GetShopItems();

        public event Action Updated;

        public ShopService(IConfigProvider configProvider, ILevelService levelService, IResourceStorage resourceStorage)
        {
            _configProvider = configProvider;
            _levelService = levelService;
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

        public ItemState TryBuy(ShopItemConfig item)
        {
            if (item.Item is not RecipeConfig recipeConfig)
                throw new ArgumentException(nameof(item));
            
            if (IsBought(item))
                return IsActive(item) ? ItemState.Selected : ItemState.Select;

            if (_levelService.Current < item.RequiredLevel)
                return (ItemState.Level);
            
            if (!_resourceStorage
                    .GetWallet(Coin)
                    .TrySpend(recipeConfig.Price))
                return ItemState.Money;
            
            return (ItemState.CanBuy);
        }

        public void Buy(ShopItemConfig item)
        {
            if (item.Item is not RecipeConfig recipeConfig)
                throw new ArgumentException(nameof(item));
            
            if(TryBuy(item) != ItemState.CanBuy)
                return;
            
            _resourceStorage
                .GetWallet(Coin)
                .Spend(recipeConfig.Price);

            CompleteBuyingProcess(item);
        }

        public abstract bool IsBought(ShopItemConfig item);
        public abstract bool IsActive(ShopItemConfig item);
        protected abstract void CompleteBuyingProcess(ShopItemConfig item);

        public void Dispose()
        {
            _disposables.Dispose();
            _levelService.LevelChanged -= OnLevelChanged;
        }

        private void OnMoneyChanged(int value) => Updated?.Invoke();
        private void OnLevelChanged(int current, int next) => Updated?.Invoke();
    }
}