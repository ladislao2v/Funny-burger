using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Shop
{
    public sealed class ItemsView : View, IItemsView
    {
        [SerializeField] private Transform _container;
        [SerializeField] private ScrollRect _scrollRect;

        private readonly List<IItemView> _shopItemViews = new();
        private Coroutine _refreshScrollCoroutine;
        
        public IEnumerable<IItemView> ItemViews => _shopItemViews;

        public void Show(IEnumerable<IItemView> shopItemsView)
        {
            foreach (Transform child in _container)
                Destroy(child.gameObject);
            
            _shopItemViews.Clear();
            _shopItemViews.AddRange(shopItemsView);
            _shopItemViews.ForEach(x => x.SetParent(_container));

            if (_scrollRect == null)
                return;

            if (_refreshScrollCoroutine != null)
                StopCoroutine(_refreshScrollCoroutine);

            _refreshScrollCoroutine = StartCoroutine(RefreshScrollNextFrame());
        }

        private IEnumerator RefreshScrollNextFrame()
        {
            yield return null;

            Canvas.ForceUpdateCanvases();

            if (_container is RectTransform contentRect)
                LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);

            Canvas.ForceUpdateCanvases();
            _scrollRect.StopMovement();
            _scrollRect.verticalNormalizedPosition = 1f;
            _refreshScrollCoroutine = null;
        }
    }
}
