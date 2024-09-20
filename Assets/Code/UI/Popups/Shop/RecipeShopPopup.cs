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

            foreach (var item in items.OrderByDescending(x => _recipeShop.TryBuy(x as RecipeConfig) == ItemState.InMenu))
            {
                IShopItemView shopItemView = await 
                    _factory.Create(item);

                RecipeConfig recipeConfig = item as RecipeConfig;
                
                shopItemView.ChangeItemState(_recipeShop.TryBuy(recipeConfig));
                
                shopItemView.BuyButtonClicked += OnBuyButtonClicked;

                itemViews.Add(shopItemView);
            }
            
            _shopView.Show(itemViews);
        }

        private void OnBuyButtonClicked(IShopItem item)
        {
            if(item is not RecipeConfig recipe)
                return;

            if(_recipeShop.TryBuy(recipe) != ItemState.CanBuy)
                return;
            
            _recipeShop.Buy(recipe);
        }
        
        private void OnShopUpdated()
        {
            if(_shopView.ItemViews.IsEmpty())
                return;

            var items = _shopView.ItemViews
                .Select(x => x.ShopItem)
                .OrderByDescending(x => _recipeShop.TryBuy(x as RecipeConfig) == ItemState.InMenu)
                .Cast<RecipeConfig>()
                .ToList();
            
            int i = 0;
            
            foreach (var itemView in _shopView.ItemViews)
            {
                itemView.Construct(items[i]);
                itemView.ChangeItemState(_recipeShop.TryBuy(items[i++]));
            }
        }
    }
}