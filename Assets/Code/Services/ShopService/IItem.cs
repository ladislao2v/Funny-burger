using UnityEngine;

namespace Code.Services.ShopService
{
    public interface IItem
    {
        public Sprite Logo { get; }
        string Name { get; }
        public int RequiredLevel { get; }
        public int Price { get; }
    }
}