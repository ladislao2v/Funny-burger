using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Services.ConfigProvider;
using Code.Services.LevelService;
using Code.Services.LocationsService;
using Code.Services.ResourceStorage;

namespace Code.Services.ShopService
{
    public class LocationsShopService : ShopService, ILocationsShopService
    {
        private readonly ILocationsService _locationsService;

        public LocationsShopService(
            IConfigProvider configProvider,
            ILevelService levelService,
            IResourceStorage resourceStorage,
            ILocationsService locationsService) : base(configProvider, levelService, resourceStorage)
        {
            _locationsService = locationsService;
        }

        public override IEnumerable<ShopItemConfig> GetShopItemsByType(ShopType shopType) =>
            shopType switch
            {
                ShopType.LocationShop => ConfigProvider.GetShopItemsByType(shopType).Where(x => _locationsService.OpenedLocations.Contains(x.Item)),
                _ => throw new ArgumentException(nameof(shopType))
            };

        protected override void OnInitialize()
        {
            _locationsService.LocationOpened += OnLocationOpened;
            _locationsService.LocationChanged += OnLocationChanged;
        }

        protected override void OnDispose()
        {
            _locationsService.LocationOpened -= OnLocationOpened;
            _locationsService.LocationChanged -= OnLocationChanged;
        }

        public override void Apply(ShopItemConfig shopItem)
        {
            if (shopItem.Item is LocationConfig locationConfig)
                _locationsService.TryUseLocation(locationConfig.LocationId);
        }

        public override bool IsBought(ShopItemConfig item) =>
            _locationsService.PurchasedLocations.Contains(item.Item);

        public override bool IsActive(ShopItemConfig item) =>
            _locationsService.CurrentLocation == item.Item;

        protected override bool ValidateItem(ShopItemConfig item) =>
            item.Item is LocationConfig locationConfig &&
            _locationsService.OpenedLocations.Contains(locationConfig);

        protected override void GetItem(ShopItemConfig item)
        {
            if (item.Item is LocationConfig locationConfig)
                _locationsService.TryPurchaseLocation(locationConfig);
        }

        private void OnLocationOpened(LocationConfig locationConfig) => UpdateShop();
        private void OnLocationChanged(LocationConfig locationConfig) => UpdateShop();
    }
}
