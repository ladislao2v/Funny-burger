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
    public sealed class ShopPopup : Popup
    {
        private readonly CompositeDisposable _disposables = new();

        private IShopService _shop;
        private IItemViewFactory _factory;
        private IItemsView _itemsView;

        [Inject]
        private void Construct(IShopService shop, IItemViewFactory factory)
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

        private async void CreateShopItems(IEnumerable<ShopItemConfig> items)
        {
            List<IItemView> itemViews = new();

            foreach (var item in items.OrderByDescending(x => x.Price))
            {
                IItemView itemView = await 
                    _factory.Create(item.Item, item.RequiredLevel, item.Currency, item.Price);
                
                itemView.ChangeItemState(_shop.TryBuy(item));
                
                itemView.BuyButtonClicked += OnBuyButtonClicked;

                itemViews.Add(itemView);
            }
            
            _itemsView.Show(itemViews);
        }

        private void OnBuyButtonClicked(Item item)
        {
            ShopItemConfig shopItem = GetShopItemConfigByItem(item);
            
            if(_shop.TryBuy(shopItem) != ItemState.CanBuy)
                return;
            
            _shop.Buy(shopItem);
        }
        
        private void OnShopUpdated()
        {
            if(_itemsView.ItemViews.IsEmpty())
                return;

            var items = _itemsView.ItemViews
                .Select(x => x.Item)
                .OrderByDescending(x => _shop.TryBuy(GetShopItemConfigByItem(x)) == ItemState.Selected)
                .ToList();
            
            int i = 0;
            
            foreach (IItemView itemView in _itemsView.ItemViews)
            {
                itemView.Construct(items[i]);
                itemView.ChangeItemState(_shop.TryBuy(GetShopItemConfigByItem(items[i++])));
            }
        }

        private ShopItemConfig GetShopItemConfigByItem(Item item) => _shop
            .AllItems.
            FirstOrDefault(x => x.Item == item);
    }
}
