using Code.Services.ShopService;
using Code.UI.Shop;
using Cysharp.Threading.Tasks;

namespace Code.Services.Factories.ItemShopFactory
{
    public interface IShopItemViewFactory
    {
        UniTask<IItemView> Create(IItem item);
    }
}