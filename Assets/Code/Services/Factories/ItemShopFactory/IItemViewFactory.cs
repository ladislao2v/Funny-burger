using Code.Configs;
using Code.Services.ResourceStorage;
using Code.UI.Shop;
using Cysharp.Threading.Tasks;

namespace Code.Services.Factories.ItemShopFactory
{
    public interface IItemViewFactory
    {
        UniTask<IItemView> Create(Item item, int? level = null, ResourceType? currency = null, int? price = null);
    }
}