using System.Collections.Generic;
using UnityEngine;

namespace Code.UI.Shop
{
    public sealed class ItemsView : View, IItemsView
    {
        [SerializeField] private Transform _container;

        private readonly List<IItemView> _shopItemViews = new();
        
        public IEnumerable<IItemView> ItemViews => _shopItemViews;

        public void Show(IEnumerable<IItemView> shopItemsView)
        {
            _shopItemViews.Clear();
            _shopItemViews.AddRange(shopItemsView);
            _shopItemViews.ForEach(x => x.SetParent(_container));
        }
    }
}