using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Services.ConfigProvider;
using Code.Services.LevelService;
using Code.Services.ResourceStorage;
using Code.Services.SkinsService;

namespace Code.Services.ShopService
{
    public class SkinsShopService : ShopService
    {
        private readonly ISkinsService _skinsService;

        public SkinsShopService(
            IConfigProvider configProvider, 
            ILevelService levelService, 
            IResourceStorage resourceStorage,
            ISkinsService skinsService) : base(configProvider, levelService, resourceStorage)
        {
            _skinsService = skinsService;
        }

        public override IEnumerable<ShopItemConfig> GetShopItemsByType(ShopType shopType) =>
            shopType switch
            {
                ShopType.HatSkinShop => ConfigProvider.GetShopItemsByType(shopType),
                ShopType.BodySkinShop  => ConfigProvider.GetShopItemsByType(shopType).Where(x => _skinsService.OpenedBodySkins.Contains(x.Item)),
                _ => throw new ArgumentException(nameof(shopType))
            };

        protected override void OnInitialize()
        {
            _skinsService.BodySkinChanged += OnBodySkinChanged;
            _skinsService.HatSkinChanged += OnHatSkinChanged;
        }

        protected override void OnDispose()
        {
            _skinsService.BodySkinChanged -= OnBodySkinChanged;
            _skinsService.HatSkinChanged -= OnHatSkinChanged;
        }

        public override void Apply(ShopItemConfig shopItem)
        {
            switch (shopItem.Item)
            {
                case BodySkinConfig bodySkinConfig:
                    _skinsService.TryUseBodySkin(bodySkinConfig.BodySkinId);
                    break;
                case HatSkinConfig hatSkinConfig:
                    _skinsService.TryUseHatSkin(hatSkinConfig.HatSkinId);
                    break;
            }
        }

        public override bool IsBought(ShopItemConfig item) => 
            _skinsService.OpenedBodySkins.Contains(item.Item) || 
            _skinsService.OpenedHatSkin.Contains(item.Item);

        public override bool IsActive(ShopItemConfig item) => 
            _skinsService.CurrentBodySkin == item.Item ||  
            _skinsService.CurrentHatSkin == item.Item;

        protected override bool ValidateItem(ShopItemConfig item) => 
            item.Item is BodySkinConfig || 
            item.Item is HatSkinConfig;

        protected override void GetItem(ShopItemConfig item)
        {
            switch (item.Item)
            {
                case BodySkinConfig config:
                    _skinsService.OpenNewBodySkinConfig(config);
                    break;
                case HatSkinConfig config:
                    _skinsService.OpenNewHatSkinConfig(config);
                    break;
            }
        }

        private void OnBodySkinChanged(BodySkinConfig bodySkinConfig) => UpdateShop();
        private void OnHatSkinChanged(HatSkinConfig hatSkinConfig) => UpdateShop();
    }
}