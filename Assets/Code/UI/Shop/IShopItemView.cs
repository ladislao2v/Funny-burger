using System;
using Code.Services.ShopService;
using UnityEngine;

namespace Code.UI.Shop
{
    public interface IShopItemView : IView
    {
        public IShopItem ShopItem { get; }
        bool IsActive { get; }

        event Action<IShopItem> BuyButtonClicked;
        void Construct(IShopItem shopItem);
        void SetParent(Transform parent);
        void EnableButton();
        void DisableButton();
    }
}