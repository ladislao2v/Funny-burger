using System;
using System.Collections.Generic;
using Code.Configs;
using Code.UI.Shop;

namespace Code.Services.ShopService
{
    public interface IShopService
    {
        IEnumerable<IItem>  AllItems { get; }

        event Action Updated;

        ItemState TryBuy(IItem item);
        void Buy(IItem item);
        bool IsBought(IItem item);
    }
}