using System.Collections.Generic;
using Code.Configs;
using Code.Services.Factories.ItemShopFactory;
using Code.Services.ShopService;
using ModestTree;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.UI.Popups.Shop
{
    public sealed class RecipeShopPopup : Popup
    {
        private readonly CompositeDisposable _disposables = new();
        
        [SerializeField] private ShopPanel _shopPanel;

        private IRecipeShopService _recipeShop;
        private IShopItemViewFactory _factory;

        [Inject]
        private void Construct(IRecipeShopService recipeShop, IShopItemViewFactory factory)
        {
            _factory = factory;
            _recipeShop = recipeShop;
        }

        private void OnEnable()
        {
            CreateShopItems(_recipeShop.AllRecipes);
            _recipeShop.Updated += OnShopUpdated;
        }

        private void OnDisable()
        {
            _disposables.Dispose();

            foreach (IShopItemView itemView in _shopPanel.ItemViews)
                itemView.BuyButtonClicked -= TryBuy;
            
            _recipeShop.Updated -= OnShopUpdated;
        }

        private async void CreateShopItems(IEnumerable<IShopItem> items)
        {
            List<IShopItemView> itemViews = new();

            foreach (var item in items)
            {
                IShopItemView shopItemView = await 
                    _factory.Create(item);

                if(_recipeShop.TryBuy(item as RecipeConfig))
                    shopItemView.EnableButton();
                else
                    shopItemView.DisableButton();
                
                shopItemView.BuyButtonClicked += TryBuy;

                itemViews.Add(shopItemView);
            }
            
            _shopPanel.Show(itemViews);
        }

        private void TryBuy(IShopItem item)
        {
            if(item is not RecipeConfig recipe)
                return;

            if(!_recipeShop.TryBuy(recipe))
                return;
            
            _recipeShop.Buy(recipe);
        }
        
        private void OnShopUpdated()
        {
            if(_shopPanel.ItemViews.IsEmpty())
                return;

            foreach (var itemView in _shopPanel.ItemViews)
            {
                if(_recipeShop.TryBuy(itemView.ShopItem as RecipeConfig))
                    itemView.EnableButton();
                else
                    itemView.DisableButton();
            }
            
            _shopPanel.Sort();
        }
    }
}