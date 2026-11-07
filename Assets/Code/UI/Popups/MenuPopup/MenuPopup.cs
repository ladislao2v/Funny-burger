using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Services.ConfigProvider;
using Code.Services.Factories.ItemShopFactory;
using Code.Services.LevelService;
using Code.Services.RecipeService;
using Code.UI.Shop;
using Zenject;

namespace Code.UI.Popups.MenuPopup
{
    public sealed class MenuPopup : Popup
    {
        private IItemViewFactory _factory;
        private IItemsView _itemsView;
        private IRecipeService _recipeService;
        private IConfigProvider _configProvider;
        private ILevelService _levelService;

        [Inject]
        private void Construct(
            IRecipeService recipeService, 
            IConfigProvider configProvider,
            ILevelService levelService,
            IItemViewFactory factory)
        {
            _levelService = levelService;
            _recipeService = recipeService;
            _configProvider = configProvider;
            _factory = factory;
            _itemsView = GetComponent<IItemsView>();
        }

        private void OnEnable() => 
            CreateRecipesIcons();
            

        private async void CreateRecipesIcons()
        {
            List<IItemView> itemViews = new();
            
            foreach (var item in _recipeService.Storage)
            {
                IItemView itemView = await 
                    _factory.Create(item);
                
                itemView.ChangeItemState( ItemState.Selected);
                
                itemViews.Add(itemView);
            }
            
            var rewardsByLevel = _configProvider
                .RewardsConfig
                .RewardsByLevel
                .Where(x => x.Value.Item is RecipeConfig)
                .Where(x => x.Key > _levelService.Current)
                .OrderBy(x => x.Key);

            foreach (var rewardByLevel in rewardsByLevel)
            {
                IItemView itemView = await 
                    _factory.Create(rewardByLevel.Value.Item, level: rewardByLevel.Key);
                
                itemView.ChangeItemState(ItemState.Level);
                
                itemViews.Add(itemView);
            }
            
            _itemsView.Show(itemViews);
        }
    }
}