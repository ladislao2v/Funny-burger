using System;
using Code.Services.ShopService;
using UnityEngine;

namespace Code.UI.Shop
{
    public interface IItemView : IView
    {
        public IItem Item { get; }
        event Action<IItem> BuyButtonClicked;
        event Action<IItem> SelectButtonClicked;

        void Construct(IItem item);
        void SetParent(Transform parent);
        void ChangeItemState(ItemState state);
    }
}