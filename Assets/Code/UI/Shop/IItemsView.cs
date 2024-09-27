using System.Collections.Generic;

namespace Code.UI.Shop
{
    public interface IItemsView : IView
    {
        IEnumerable<IItemView> ItemViews { get; }
        void Show(IEnumerable<IItemView> shopItemViews);
    }
}