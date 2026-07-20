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

        public override bool IsBought(ShopItemConfig item) => 
            _skinsService.OpenedBodySkins.Contains(item.Item) || 
            _skinsService.OpenedHatSkin.Contains(item.Item);

        public override bool IsActive(ShopItemConfig item) => 
            _skinsService.CurrentBodySkin == item.Item ||  
            _skinsService.CurrentHatSkin == item.Item;

        protected override void CompleteBuyingProcess(ShopItemConfig item)
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
    }
}