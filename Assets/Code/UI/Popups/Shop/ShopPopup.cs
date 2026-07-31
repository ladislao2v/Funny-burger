using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Services.Factories.ItemShopFactory;
using Code.Services.ShopService;
using Code.UI.ActiveSkins;
using Code.UI.Shop;
using ModestTree;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Popups.Shop
{
    public sealed class ShopPopup : Popup
    {
        private readonly CompositeDisposable _disposables = new();
        
        [SerializeField] private ActiveSkinsBar _activeSkinsBar;
        [SerializeField] private Toggle _hatsListToggle;
        [SerializeField] private Toggle _bodyListToggle;

        private ShopType _currentShopType = ShopType.HatSkinShop;
        
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
            _hatsListToggle.onValueChanged.AddListener(OnHatsListToggleValueChanged);
            _bodyListToggle.onValueChanged.AddListener(OnBodyListToggleValueChanged);
            
            CreateShopItems(_shop.GetShopItemsByType(_currentShopType));
            _shop.Updated += OnShopUpdated;
        }

        private void OnDisable()
        {
            _hatsListToggle.onValueChanged.RemoveAllListeners();
            _bodyListToggle.onValueChanged.RemoveAllListeners();
            
            _disposables.Dispose();

            foreach (IItemView itemView in _itemsView.ItemViews)
                itemView.BuyButtonClicked -= OnBuyButtonClicked;
            
            _shop.Updated -= OnShopUpdated;
        }

        private async void CreateShopItems(IEnumerable<ShopItemConfig> items)
        {
            List<IItemView> itemViews = new();

            foreach (var item in items.OrderBy(x => x.RequiredLevel))
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

            switch (_shop.TryBuy(shopItem))
            {
                case ItemState.CanBuy:
                    _shop.Buy(shopItem);
                    break;
                case ItemState.Select:
                    _shop.Apply(shopItem);
                    break;
            }
        }

        private void OnShopUpdated()
        {
            if(_itemsView.ItemViews.IsEmpty())
                return;
            
            foreach (IItemView itemView in _itemsView.ItemViews)
            {
                itemView.ChangeItemState(_shop.TryBuy(GetShopItemConfigByItem(itemView.Item)));
            }
        }

        private ShopItemConfig GetShopItemConfigByItem(Item item) => _shop
            .GetShopItemsByType(_currentShopType).
            FirstOrDefault(x => x.Item == item);

        private void OnBodyListToggleValueChanged(bool active)
        {
            if (!active)
                return;

            _currentShopType = ShopType.BodySkinShop;
            
            CreateShopItems(_shop.GetShopItemsByType(_currentShopType));
        }

        private void OnHatsListToggleValueChanged(bool active)
        {
            if (!active)
                return;

            _currentShopType = ShopType.HatSkinShop;
            
            CreateShopItems(_shop.GetShopItemsByType(_currentShopType));
        }
    }
}
