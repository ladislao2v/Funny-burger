using System;
using Code.Configs;
using Code.Services.ResourceStorage;
using UnityEngine;

namespace Code.UI.Shop
{
    public interface IItemView : IView
    {
        public Item Item { get; }
        event Action<Item> BuyButtonClicked;
        event Action<Item> SelectButtonClicked;

        void Construct(Item item, int? level = null, ResourceType? currency = null,int? price = null);
        void SetParent(Transform parent);
        void ChangeItemState(ItemState state);
    }
}