using System.Collections.Generic;

namespace Code.UI.Shop
{
    public interface IShopView : IView
    {
        IEnumerable<IShopItemView> ItemViews { get; }
        void Show(IEnumerable<IShopItemView> shopItemViews);
        void Sort();
    }
}