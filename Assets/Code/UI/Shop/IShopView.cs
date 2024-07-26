using System.Collections.Generic;

namespace Code.UI.Shop
{
    public interface IShopView : IView
    {
        void Show(IEnumerable<IShopItemView> shopItemViews);
        void Sort();
    }
}