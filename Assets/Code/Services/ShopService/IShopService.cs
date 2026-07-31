using System;
using System.Collections.Generic;
using Code.Configs;
using Code.UI.Shop;

namespace Code.Services.ShopService
{
    public interface IShopService
    {
        event Action Updated;

        ItemState TryBuy(ShopItemConfig item);
        void Buy(ShopItemConfig item);
        void Apply(ShopItemConfig shopItem);
        bool IsBought(ShopItemConfig item);
        bool IsActive(ShopItemConfig item);
        IEnumerable<ShopItemConfig> GetShopItemsByType(ShopType shopType);
    }
}