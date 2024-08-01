﻿using System.Collections.Generic;
using Code.Configs;
using Code.Services.Factories.ItemShopFactory;
using Code.Services.ShopService;
using Code.UI.Shop;
using ModestTree;
using UniRx;
using Unity.VisualScripting;
using Zenject;

namespace Code.UI.Popups.Shop
{
    public sealed class RecipeShopPopup : Popup
    {
        private readonly CompositeDisposable _disposables = new();

        private IRecipeShopService _recipeShop;
        private IShopItemViewFactory _factory;
        private IShopView _shopView;

        [Inject]
        private void Construct(IRecipeShopService recipeShop, IShopItemViewFactory factory)
        {
            _factory = factory;
            _recipeShop = recipeShop;
            _shopView = GetComponent<IShopView>();
        }

        private void OnEnable()
        {
            CreateShopItems(_recipeShop.AllRecipes);
            _recipeShop.Updated += OnShopUpdated;
        }

        private void OnDisable()
        {
            _disposables.Dispose();

            foreach (IShopItemView itemView in _shopView.ItemViews)
                itemView.BuyButtonClicked -= OnBuyButtonClicked;
            
            _recipeShop.Updated -= OnShopUpdated;
        }

        private async void CreateShopItems(IEnumerable<IShopItem> items)
        {
            List<IShopItemView> itemViews = new();

            foreach (var item in items)
            {
                IShopItemView shopItemView = await 
                    _factory.Create(item);

                RecipeConfig recipeConfig = item as RecipeConfig;

                if(_recipeShop.TryBuy(recipeConfig))
                    shopItemView.EnableButton();
                else
                    shopItemView.DisableButton(item.IsStart || _recipeShop.IsBought(recipeConfig));
                
                shopItemView.BuyButtonClicked += OnBuyButtonClicked;

                itemViews.Add(shopItemView);
            }
            
            _shopView.Show(itemViews);
        }

        private void OnBuyButtonClicked(IShopItem item)
        {
            if(item is not RecipeConfig recipe)
                return;

            if(!_recipeShop.TryBuy(recipe))
                return;
            
            _recipeShop.Buy(recipe);
        }
        
        private void OnShopUpdated()
        {
            if(_shopView.ItemViews.IsEmpty())
                return;

            foreach (var itemView in _shopView.ItemViews)
            {
                RecipeConfig recipe = itemView.ShopItem as RecipeConfig; 
                
                if (_recipeShop.TryBuy(recipe))
                    itemView.EnableButton();
                else
                    itemView.DisableButton(_recipeShop.IsBought(recipe));
            }
        }
    }
}