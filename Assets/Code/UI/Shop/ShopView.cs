using System.Collections.Generic;
using UnityEngine;

namespace Code.UI.Shop
{
    public sealed class ShopView : View, IShopView
    {
        [SerializeField] private Transform _container;

        private List<IShopItemView> _shopItemViews = new();
        
        public IEnumerable<IShopItemView> ItemViews => _shopItemViews;

        public void Show(IEnumerable<IShopItemView> shopItemsView)
        {
            _shopItemViews.Clear();
            _shopItemViews.AddRange(shopItemsView);
            _shopItemViews.ForEach(x => x.SetParent(_container));
        }
    }
}