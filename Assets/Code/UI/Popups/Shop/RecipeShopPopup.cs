using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Services.Factories.ItemShopFactory;
using Code.Services.ShopService;
using Code.UI.Shop;
using ModestTree;
using UniRx;
using Zenject;

namespace Code.UI.Popups.Shop
{
    public sealed class RecipeShopPopup : Popup
    {
        private readonly CompositeDisposable _disposables = new();

        private IShopService _shop;
        private IShopItemViewFactory _factory;
        private IItemsView _itemsView;

        [Inject]
        private void Construct(IShopService shop, IShopItemViewFactory factory)
        {
            _factory = factory;
            _shop = shop;
            _itemsView = GetComponent<IItemsView>();
        }

        private void OnEnable()
        {
            CreateShopItems(_shop.AllItems);
            _shop.Updated += OnShopUpdated;
        }

        private void OnDisable()
        {
            _disposables.Dispose();

            foreach (IItemView itemView in _itemsView.ItemViews)
                itemView.BuyButtonClicked -= OnBuyButtonClicked;
            
            _shop.Updated -= OnShopUpdated;
        }

        private async void CreateShopItems(IEnumerable<IItem> items)
        {
            List<IItemView> itemViews = new();

            foreach (var item in items.OrderByDescending(x => _shop.TryBuy(x as RecipeConfig) == ItemState.Selected))
            {
                IItemView itemView = await 
                    _factory.Create(item);

                RecipeConfig recipeConfig = item as RecipeConfig;
                
                itemView.ChangeItemState(_shop.TryBuy(recipeConfig));
                
                itemView.BuyButtonClicked += OnBuyButtonClicked;

                itemViews.Add(itemView);
            }
            
            _itemsView.Show(itemViews);
        }

        private void OnBuyButtonClicked(IItem item)
        {
            if(item is not RecipeConfig recipe)
                return;

            if(_shop.TryBuy(recipe) != ItemState.CanBuy)
                return;
            
            _shop.Buy(recipe);
        }
        
        private void OnShopUpdated()
        {
            if(_itemsView.ItemViews.IsEmpty())
                return;

            var items = _itemsView.ItemViews
                .Select(x => x.Item)
                .OrderByDescending(x => _shop.TryBuy(x as RecipeConfig) == ItemState.Selected)
                .Cast<RecipeConfig>()
                .ToList();
            
            int i = 0;
            
            foreach (IItemView itemView in _itemsView.ItemViews)
            {
                itemView.Construct(items[i]);
                itemView.ChangeItemState(_shop.TryBuy(items[i++]));
            }
        }
    }
}