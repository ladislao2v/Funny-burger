using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.UI.Popups.Shop
{
    public sealed class ShopPanel : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private List<IShopItemView> _shopItemViews = new();
        
        public IEnumerable<IShopItemView> ItemViews => _shopItemViews;

        public void Show(IEnumerable<IShopItemView> shopItemsView)
        {
            _shopItemViews.AddRange(shopItemsView);
            _shopItemViews.ForEach(x => x.SetParent(_container));
            
            Sort();
        }

        public void Sort() =>
            _shopItemViews = _shopItemViews
                .OrderByDescending(x => x.IsActive)
                .ToList();
    }
}