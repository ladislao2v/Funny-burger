using Code.Services.PopupService;
using Code.Services.RecipeService;
using Code.Services.ResourceStorage;
using Code.Services.SkinsService;
using Code.UI.Popups.Reward;

namespace Code.Configs.ItemVisitors
{
    public sealed class ItemGiverVisitor : IItemVisitor
    {
        private readonly IResourceStorage _resourceStorage;
        private readonly IRecipeService _recipeService;
        private readonly IPopupService _popupService;
        private readonly ISkinsService _skinsService;

        public ItemGiverVisitor(
            IResourceStorage resourceStorage, 
            IRecipeService recipeService, 
            IPopupService popupService,
            ISkinsService skinsService)
        {
            _resourceStorage = resourceStorage;
            _recipeService = recipeService;
            _popupService = popupService;
            _skinsService = skinsService;
        }
        
        public void Visit(RecipeConfig recipeConfig) => 
            _recipeService.AddRecipe(recipeConfig);

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
            IPopupData data = new LocationRewardData();
            
            _popupService.ShowPopup(PopupType.Reward, data);
        }

        public void Visit(FeatureConfig featureConfig)
        {
            IPopupData data = new FeatureRewardData();
            
            _popupService.ShowPopup(PopupType.Reward, data);
        }

        public void Visit(BodySkinConfig bodySkinConfig) => 
            _skinsService.OpenNewBodySkinConfig(bodySkinConfig);

        public void Visit(HatSkinConfig hatSkinConfig) => 
            _skinsService.OpenNewHatSkinConfig(hatSkinConfig);
    }
}