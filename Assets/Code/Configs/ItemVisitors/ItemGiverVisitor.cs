using Code.Services.LocationsService;
using Code.Services.PopupService;
using Code.Services.RecipeService;
using Code.Services.ResourceStorage;
using Code.Services.SkinsService;
using Code.UI.Popups.Reward;
using Cysharp.Threading.Tasks;

namespace Code.Configs.ItemVisitors
{
    public sealed class ItemGiverVisitor : IItemVisitor
    {
        private readonly IResourceStorage _resourceStorage;
        private readonly IRecipeService _recipeService;
        private readonly IPopupService _popupService;
        private readonly ISkinsService _skinsService;
        private readonly ILocationsService _locationsService;

        public ItemGiverVisitor(
            IResourceStorage resourceStorage, 
            IRecipeService recipeService, 
            IPopupService popupService,
            ISkinsService skinsService,
            ILocationsService locationsService)
        {
            _resourceStorage = resourceStorage;
            _recipeService = recipeService;
            _popupService = popupService;
            _skinsService = skinsService;
            _locationsService = locationsService;
        }
        
        public void Visit(RecipeConfig recipeConfig)
        {
            _recipeService.AddRecipe(recipeConfig);
            
            RewardData data = new RewardData()
            {
                Name = recipeConfig.Name,
            };
            
            _popupService.ShowPopup(PopupType.Reward, data).Forget();
        }

        public void Visit(GemConfig gemConfig) =>
            _resourceStorage
                .GetWallet(ResourceType.Gem)
                .Add(gemConfig.Count);

        public void Visit(CoinConfig coinConfig)=>
            _resourceStorage
                .GetWallet(ResourceType.Coin)
                .Add(coinConfig.Count);

        public void Visit(LocationConfig locationConfig)
        {
            _locationsService.OpenNewLocation(locationConfig);
            
            RewardData data = new RewardData()
            {
                Name = locationConfig.Name,
            };
            
            _popupService.ShowPopup(PopupType.Reward, data).Forget();
        }

        public void Visit(FeatureConfig featureConfig)
        {
            RewardData data = new RewardData()
            {
                Name = featureConfig.Name,
            };
            
            _popupService.ShowPopup(PopupType.Reward, data).Forget();
        }

        public void Visit(BodySkinConfig bodySkinConfig) => 
            _skinsService.OpenNewBodySkinConfig(bodySkinConfig);

        public void Visit(HatSkinConfig hatSkinConfig) => 
            _skinsService.OpenNewHatSkinConfig(hatSkinConfig);
    }
}