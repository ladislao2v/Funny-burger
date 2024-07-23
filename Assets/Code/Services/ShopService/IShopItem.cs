using UnityEngine;

namespace Code.Services.ShopService
{
    public interface IShopItem
    {
        public Sprite Logo { get; }
        public string Description { get; }
        public int Price { get; }
    }
}