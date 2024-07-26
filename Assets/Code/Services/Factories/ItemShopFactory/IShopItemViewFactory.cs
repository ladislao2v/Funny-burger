using Code.Services.ShopService;
using Code.UI.Popups.Shop;
using Code.UI.Shop;
using Cysharp.Threading.Tasks;

namespace Code.Services.Factories.ItemShopFactory
{
    public interface IShopItemViewFactory
    {
        UniTask<IShopItemView> Create(IShopItem item);
    }
}