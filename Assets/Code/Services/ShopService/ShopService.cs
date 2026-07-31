using System;
using System.Collections.Generic;
using Code.Configs;
using Code.Services.ConfigProvider;
using Code.Services.LevelService;
using Code.Services.ResourceStorage;
using Code.UI.Shop;
using UniRx;
using UnityEngine;
using Zenject;
using static Code.Services.ResourceStorage.ResourceType;

namespace Code.Services.ShopService
{
    public abstract class ShopService : IShopService, IInitializable, IDisposable
    {
        protected readonly IConfigProvider ConfigProvider;
        
        private readonly ILevelService _levelService;
        private readonly IResourceStorage _resourceStorage;
        private readonly CompositeDisposable _disposables = new();

        public event Action Updated;

        public ShopService(IConfigProvider configProvider, ILevelService levelService, IResourceStorage resourceStorage)
        {
            ConfigProvider = configProvider;
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

            OnInitialize();
        }

        public ItemState TryBuy(ShopItemConfig item)
        {
            if (!ValidateItem(item))
                throw new ArgumentException(item.name);
            
            if (IsBought(item))
                return IsActive(item) ? ItemState.Selected : ItemState.Select;

            if (_levelService.Current < item.RequiredLevel)
                return (ItemState.Level);
            
            if (!_resourceStorage
                    .GetWallet(item.Currency)
                    .TrySpend(item.Price))
                return ItemState.Money;
            
            return (ItemState.CanBuy);
        }

        public void Buy(ShopItemConfig item)
        {
            if (!ValidateItem(item))
                throw new ArgumentException(nameof(item));
            
            if(TryBuy(item) != ItemState.CanBuy)
                return;
            
            GetItem(item);
            
            _resourceStorage
                .GetWallet(item.Currency)
                .Spend(item.Price);
        }

        public abstract void Apply(ShopItemConfig shopItem);

        public abstract bool IsBought(ShopItemConfig item);
        public abstract bool IsActive(ShopItemConfig item);
        protected abstract bool ValidateItem(ShopItemConfig item);
        protected abstract void GetItem(ShopItemConfig item);
        public abstract IEnumerable<ShopItemConfig> GetShopItemsByType(ShopType shopType);
        
        protected virtual void OnInitialize() { }
        protected virtual void OnDispose() { }

        protected void UpdateShop()
        {
            Updated?.Invoke();
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _levelService.LevelChanged -= OnLevelChanged;
            
            OnDispose();
        }
        
        private void OnMoneyChanged(int value) => UpdateShop();
        private void OnLevelChanged(int current, int next) => UpdateShop();
    }
}