using System;
using System.Collections.Generic;
using Code.Services.Factories.ItemShopFactory;
using Code.Services.RecipeService;
using Code.Services.ShopService;
using Code.UI.Shop;
using Zenject;

namespace Code.UI.Popups.MenuPopup
{
    public sealed class MenuPopup : Popup
    {
        private IShopItemViewFactory _factory;
        private IItemsView _itemsView;
        private IRecipeService _recipeService;

        [Inject]
        private void Construct(IRecipeService recipeService, IShopItemViewFactory factory)
        {
            _recipeService = recipeService;
            _factory = factory;
            _itemsView = GetComponent<IItemsView>();
        }

        private void OnEnable() => CreateRecipesIcons(_recipeService.Storage);
            

        private async void CreateRecipesIcons(IEnumerable<IItem> items)
        {
            List<IItemView> itemViews = new();

            foreach (var item in items)
            {
                IItemView itemView = await 
                    _factory.Create(item);

                itemView.ChangeItemState(ItemState.Selected);
                itemViews.Add(itemView);
            }
            
            _itemsView.Show(itemViews);
        }
    }
}