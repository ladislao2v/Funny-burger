using System;
using System.Collections.Generic;
using Code.Configs;
using Code.UI.Shop;

namespace Code.Services.ShopService
{
    public interface IShopService
    {
        IEnumerable<ShopItemConfig>  AllItems { get; }

        event Action Updated;

        ItemState TryBuy(ShopItemConfig item);
        void Buy(ShopItemConfig item);
        bool IsBought(ShopItemConfig item);
        bool IsActive(ShopItemConfig item);
    }
}